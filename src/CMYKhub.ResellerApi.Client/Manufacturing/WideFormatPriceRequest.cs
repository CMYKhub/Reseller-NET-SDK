namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class WideFormatPriceRequest : PriceRequest
    {
        public WideFormatPriceRequest()
        {
            Finishing = new WideFormatWizardFinishing[0];
        }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int Kinds { get; set; }
        public Size FinishedSize { get; set; }
        public WideFormatWizardFinishing[] Finishing { get; set; }
    }
}
