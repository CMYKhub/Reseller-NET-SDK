namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    /// <summary>
    /// Represents finishing that can be applied to a printed product
    /// </summary>
    public class Finishing : ApiResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FinishingType Type { get; set; }
    }
}
