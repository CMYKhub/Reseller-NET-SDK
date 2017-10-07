namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public abstract class PriceRequest
    {
        public string FreightProviderId { get; set; }
        public int DeliveryType { get; set; }
    }
}
