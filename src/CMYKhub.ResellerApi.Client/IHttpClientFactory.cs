using System.Net.Http;

namespace CMYKhub.ResellerApi.Client
{
    public interface IHttpClientFactory
    {
        HttpClient Create();
    }
}
