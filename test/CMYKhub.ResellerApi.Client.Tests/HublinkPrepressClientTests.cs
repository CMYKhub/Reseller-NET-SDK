using CMYKhub.ResellerApi.Client.Prepress;

namespace CMYKhub.ResellerApi.Client.Tests
{
    public abstract class HublinkPrepressClientTests : HublinkClientTests<HublinkPrepressClient>
    {
        protected override HublinkPrepressClient CreateClient()
        {
            return new HublinkPrepressClient(base.clientFactory.Object, baseUri, resellerId, apiKey);
        }
    }
}
