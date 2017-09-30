using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using CMYKhub.ResellerApi.Client.Extensions;

namespace CMYKhub.ResellerApi.Client.Prepress
{
    public class HublinkPrepressClient : HublinkClient
    {
        private const string PrepressRelation = "http://schemas.cmykhub.com/api/prepress";
        const string MediaTypeJson = "application/json";
        const int FileChunkSize = 1024000;//bytes

        public HublinkPrepressClient(IHttpClientFactory clientFactory, string baseUri, string resellerId, string apiKey)
            : base(clientFactory, baseUri, resellerId, apiKey) { }

        protected async Task<PrepressApiDiscovery> DiscoverPrepressAsync()
        {
            var rootDiscovery = await base.DiscoverAsync();
            var prepressLink = rootDiscovery.Links.Single(x => x.Relation == PrepressRelation);
            return await GetAsync<PrepressApiDiscovery>(prepressLink.Uri, new[] { new PrepressJsonMediaTypeFormatter() });
        }

        public async Task UploadArtworkAsync(string orderId, string hubId, IEnumerable<string> files)
        {
            //connect to the prepress api and upload the artwork files

            var uploadedFileUrls = new List<FileUploadResource>();
            var discovery = await DiscoverPrepressAsync();
            var uploadUrl = discovery.UploadUrl;
            var packageUrl = discovery.PackageUrl;

            foreach (var file in files)
            {
                //foreach file
                //  create an upload resource
                var mimeType = MimeMapping.GetMimeMapping(file);
                using (var stream = File.OpenRead(file))
                {
                    var resource = await CreateFileResourceAsync(uploadUrl, orderId, Path.GetFileName(file), stream.Length, mimeType);
                    uploadedFileUrls.Add(resource);

                    var md5 = MD5.Create();
                    var cs = new CryptoStream(new MemoryStream(), md5, CryptoStreamMode.Write);
                    var fileLink = resource.Links.FindLinkByRelation(PrepressApiDiscovery.Self);
                    var chunkLink = resource.Links.FindLinkByRelation(PrepressApiDiscovery.ChunksRelation);
                    var contentLink = resource.Links.FindLinkByRelation(PrepressApiDiscovery.UploadContentRelation);
                    if (fileLink == null || chunkLink == null || contentLink == null) throw new ApplicationException("Links not found");


                    byte[] buffer = new byte[FileChunkSize];
                    int bytesRead;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        var chunk = new byte[bytesRead];
                        Buffer.BlockCopy(buffer, 0, chunk, 0, bytesRead);
                        await CreateChunkResourceAsync(chunkLink.Uri, chunk);
                        cs.Write(buffer, 0, bytesRead);
                    }
                    cs.FlushFinalBlock();

                    //patch MD5
                    await PatchMD5(fileLink.Uri, BitConverter.ToString(md5.Hash).Replace("-", string.Empty).ToLower());
                }
            }
            //create prepress order resource(attaching files uploaded)
            var pkg = new ArtPackageResource
            {
                HubId = hubId,
                OrderId = orderId,
                Files = uploadedFileUrls.Select(x => new
                {
                    ContentType = x.ContentType,// "",//files.SingleOrDefault(y => Path.GetFileName(y) == x.Filename)?.MimeType,
                    Name = x.Filename,
                    Url = x.Links.FindLinkByRelation(PrepressApiDiscovery.UploadContentRelation)?.Uri
                }).ToArray()
            };
            await CreatePrepressPackageAsync(packageUrl, pkg);

        }


        private async Task<FileUploadResource> CreateFileResourceAsync(string url, string orderId, string filename, long fileLength, string contentType)
        {
            using (var client = GetClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeJson));

                var response = await client.PostAsJsonAsync(url, new { orderId, filename, fileLength, contentType });
                if (!response.IsSuccessStatusCode) return default(FileUploadResource);


                // Get the URI of the created resource.
                var result = await response.Content.ReadAsAsync<FileUploadResource>(new[] { new PrepressJsonMediaTypeFormatter() });
                return result;
            }
        }

        private async Task<bool> CreateChunkResourceAsync(string url, byte[] buffer)
        {
            using (var client = GetClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeJson));
                ByteArrayContent byteContent = new ByteArrayContent(buffer);
                var response = await client.PostAsync(url, byteContent);
                return response.IsSuccessStatusCode;
            }
        }

        private async Task<bool> PatchMD5(string url, string md5)
        {
            using (var client = GetClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeJson));
                var response = await client.PatchAsJsonAsync(url, new { MD5Hash = md5 });
                return response.IsSuccessStatusCode;
            }
        }

        private async Task<bool> CreatePrepressPackageAsync(string url, ArtPackageResource package)
        {
            using (var client = GetClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeJson));
                var response = await client.PostAsJsonAsync(url, package);
                return response.IsSuccessStatusCode;
            }
        }

    }
}
