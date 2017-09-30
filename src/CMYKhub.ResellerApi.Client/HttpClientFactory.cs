using System.Net.Http;

namespace CMYKhub.ResellerApi.Client
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient Create()
        {
            return new HttpClient();
        }
    }
}
