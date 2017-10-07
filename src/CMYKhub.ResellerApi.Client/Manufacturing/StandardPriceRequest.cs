namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class StandardPriceRequest : PriceRequest
    {
        public StandardPriceRequest()
        {
            Finishing = new PrintWizardFinishing[0];
        }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int Kinds { get; set; }
        public Size FinishedSize { get; set; }
        public PrintWizardFinishing[] Finishing { get; set; }
    }
}
