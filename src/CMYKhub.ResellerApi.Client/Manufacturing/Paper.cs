namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    /// <summary>
    /// Represents paper that can be printed on
    /// </summary>
    public class Paper : ApiResource
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Family { get; set; }
        public string Colour { get; set; }
        public string Texture { get; set; }
        public int Weight { get; set; }
    }
}
