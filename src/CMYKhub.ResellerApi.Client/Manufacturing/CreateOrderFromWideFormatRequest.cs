namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class CreateOrderFromWideFormatRequest : CreateOrderFromWizardRequest
    {
        public WideFormatPriceRequest Product { get; set; }
    }
}
