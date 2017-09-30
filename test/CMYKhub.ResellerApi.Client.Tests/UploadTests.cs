using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using CMYKhub.ResellerApi.Client.Prepress;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace CMYKhub.ResellerApi.Client.Tests
{
    [TestClass]
    public class When_upload_artwork_for_order : HublinkPrepressClientTests
    {
        private const string orderId = "139424", hubId = "6234", artworkFileId = "117e5c28-0b35-4466-9842-2605036a92b6";
        private static readonly string fileToUpload = Path.Combine(Directory.GetCurrentDirectory(), "Resources/10_5_10_5mm-Gusset-A1.pdf");
        private HttpContent createdArtworkFile, createdArtworkChunk, patchedMd5, createdOrderArtwork;
        private byte[] artworkChunkStream;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/pp/Uploads")
                .Respond(HttpStatusCode.Created, "application/json", request =>
                {
                    createdArtworkFile = request.Content;
                    return ("Resources.OrderArtwork_new.json".ReadResourceStream());
                });
            mockHttp.When(HttpMethod.Post, $"{baseUri}/pp/Uploads/{orderId}/Artwork/{artworkFileId}/Chunks")
                .Respond(HttpStatusCode.Created, "application/json", request =>
                {
                    artworkChunkStream = request.Content.ReadAsByteArrayAsync().Result;
                    createdArtworkChunk = request.Content;
                    return new MemoryStream();
                });
            mockHttp.When(new HttpMethod("PATCH"), $"{baseUri}/pp/Uploads/{orderId}/Artwork/{artworkFileId}")
                .Respond(HttpStatusCode.Created, "application/json", request =>
                {
                    patchedMd5 = request.Content;
                    return ("Resources.OrderArtwork_new.json".ReadResourceStream());
                });
            mockHttp.When(HttpMethod.Post, $"{baseUri}/pp/Orders/Artwork")
                .Respond(HttpStatusCode.Created, "application/json", request =>
                {
                    createdOrderArtwork = request.Content;
                    return new MemoryStream(Encoding.UTF8.GetBytes("{}"));
                });

            Act();
        }

        public override void Act()
        {
            var t = client.UploadArtworkAsync(orderId, hubId, new[] { fileToUpload });
            t.Wait();
        }

        [TestMethod]
        public void Should_create_artwork_file()
        {
            Assert.IsNotNull(createdArtworkFile);
        }

        [TestMethod]
        public void Created_artwork_file_should_have_orderId()
        {
            var createdObject = createdArtworkFile.ReadAsAsync<dynamic>().Result;
            Assert.AreEqual(orderId, createdObject.orderId);
        }

        [TestMethod]
        public void Created_artwork_file_should_have_filename()
        {
            var createdObject = createdArtworkFile.ReadAsAsync<dynamic>().Result;
            Assert.AreEqual("10_5_10_5mm-Gusset-A1.pdf", createdObject.filename);
        }

        [TestMethod]
        public void Created_artwork_file_should_have_fileLength()
        {
            var createdObject = createdArtworkFile.ReadAsAsync<dynamic>().Result;
            Assert.AreEqual(87534L, createdObject.fileLength);
        }

        [TestMethod]
        public void Created_artwork_file_should_have_contentType()
        {
            var createdObject = createdArtworkFile.ReadAsAsync<dynamic>().Result;
            Assert.AreEqual("application/pdf", createdObject.contentType);
        }

        [TestMethod]
        public void Should_create_artwork_file_chunk()
        {
            Assert.IsNotNull(createdArtworkChunk);
        }

        [TestMethod]
        public void Created_artwork_chunk_should_have_correct_contents()
        {
            Assert.AreEqual(87534L, artworkChunkStream.LongLength);
        }

        [TestMethod]
        public void Should_create_patch_md5()
        {
            Assert.IsNotNull(patchedMd5);
        }

        [TestMethod]
        public void Patch_MD5_should_have_correct_MD5()
        {
            var patchedObject = patchedMd5.ReadAsAsync<dynamic>().Result;
            Assert.AreEqual("016450376e96002d381c8befc95709a8", patchedObject.MD5Hash);
        }

        [TestMethod]
        public void Should_create_order_artwork()
        {
            Assert.IsNotNull(createdOrderArtwork);
        }

        [TestMethod]
        public void Created_order_artwork_should_have_file_Name()
        {
            var orderArtwork = createdOrderArtwork.ReadAsAsync<ArtPackageResource>().Result;
            Assert.AreEqual("10_5_10_5mm-Gusset-A1.pdf", ((dynamic)orderArtwork.Files.Single()).Name);
        }

        [TestMethod]
        public void Created_order_artwork_should_have_file_ContentType()
        {
            var orderArtwork = createdOrderArtwork.ReadAsAsync<ArtPackageResource>().Result;
            Assert.AreEqual("application/pdf", ((dynamic)orderArtwork.Files.Single()).ContentType);
        }

        [TestMethod]
        public void Created_order_artwork_should_have_file_Url()
        {
            var orderArtwork = createdOrderArtwork.ReadAsAsync<ArtPackageResource>().Result;
            Assert.AreEqual($"{baseUri}/pp/Uploads/{orderId}/Artwork/{artworkFileId}/Content", ((dynamic)orderArtwork.Files.Single()).Url);
        }

        [TestMethod]
        public void Created_order_artwork_should_have_OrderId()
        {
            var orderArtwork = createdOrderArtwork.ReadAsAsync<ArtPackageResource>().Result;
            Assert.AreEqual(orderId, orderArtwork.OrderId);
        }

        [TestMethod]
        public void Created_order_artwork_should_have_HubId()
        {
            var orderArtwork = createdOrderArtwork.ReadAsAsync<ArtPackageResource>().Result;
            Assert.AreEqual(hubId, orderArtwork.HubId);
        }
    }

}
