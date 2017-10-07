namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class FinishingAvailableSpec
    {
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public PrintTypes PrintType { get; set; }
        public int PaperWeight { get; set; }
        public FinishingAvailableBookletSpec Book { get; set; }
    }
}
