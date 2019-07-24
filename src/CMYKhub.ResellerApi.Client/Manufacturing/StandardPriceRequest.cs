using System.Collections.Generic;

namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class StandardPriceRequest : PriceRequest
    {
        public StandardPriceRequest()
        {
            Kinds = 1;
        }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int Kinds { get; set; }
        public Size FinishedSize { get; set; }
        public PrintWizardFinishing[] Finishing { get; set; }
    }
}
