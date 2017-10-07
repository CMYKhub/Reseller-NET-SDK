namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public ProductGroup ProductGroup { get; set; }
    }
}
