namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    /// <summary>
    /// Represents a standard product that is printed on a ganged print run
    /// </summary>
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public ProductGroup ProductGroup { get; set; }
    }
}
