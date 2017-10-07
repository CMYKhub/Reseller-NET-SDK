namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class Finishing : ApiResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FinishingType Type { get; set; }
    }
}
