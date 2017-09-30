using CMYKhub.ResellerApi.Client.Manufacturing;

namespace CMYKhub.ResellerApi.Client.Tests
{
    public abstract class HublinkManufacturingClientTests : HublinkClientTests<HublinkManufacturingClient>
    {
        protected override HublinkManufacturingClient CreateClient()
        {
            return new HublinkManufacturingClient(base.clientFactory.Object, baseUri, resellerId, apiKey);
        }
    }
}
