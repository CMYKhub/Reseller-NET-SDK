namespace CMYKhub.ResellerApi.Client
{
    public class ClientSettings
    {
        public ClientSettings(string baseUri, string resellerId, string apiKey)
        {
            this.BaseUri = baseUri;
            this.ResellerId = resellerId;
            this.ApiKey = apiKey;
        }

        public string BaseUri { get; }

        public string ResellerId { get; }

        public string ApiKey { get; }
    }
}
