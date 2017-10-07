namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class BookletProductRequest : PriceRequest
    {
        public int Quantity { get; set; }
        public Orientations Orientation { get; set; }
        public Size FinishedSize { get; set; }
        public string BindingId { get; set; }
        public BookletBodySection Body { get; set; }
        public BookletCoverSection Cover { get; set; }
        public int PrintType { get; set; }
    }
}
