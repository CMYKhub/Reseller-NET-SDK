﻿namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class WideFormatPriceRequest : PriceRequest
    {
        public WideFormatPriceRequest()
        {
            Kinds = 1;
        }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int Kinds { get; set; }
        public Size FinishedSize { get; set; }
        public WideFormatWizardFinishing[] Finishing { get; set; }
    }
}
