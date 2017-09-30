using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CMYKhub.ResellerApi.Client.Manufacturing;

namespace CMYKhub.ResellerApi.Client
{
    public class HublinkClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _baseUri;
        private readonly string _resellerId;
        private readonly string _apiKey;
        private Discovery _discovery;

        public HublinkClient(IHttpClientFactory clientFactory, string baseUri, string resellerId, string apiKey)
        {
            _clientFactory = clientFactory;
            _baseUri = baseUri;
            _resellerId = resellerId;
            _apiKey = apiKey;
        }

        protected HttpClient GetClient()
        {
            var client = _clientFactory.Create();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.cmykhub+json"));
            client.DefaultRequestHeaders.Add("ResellerId", _resellerId);
            client.DefaultRequestHeaders.Add("APIKey", _apiKey);

            return client;
        }

        protected async Task<T> GetAsync<T>(string uri, IEnumerable<MediaTypeFormatter> formatters = null)
        {
            var client = GetClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
                if (formatters == null)
                    return await response.Content.ReadAsAsync<T>();
                else
                    return await response.Content.ReadAsAsync<T>(formatters);
            else
                return default(T);
        }

        protected async Task<Discovery> DiscoverAsync()
        {
            return _discovery ?? (_discovery = await GetAsync<Discovery>(_baseUri));
        }
    }
}
