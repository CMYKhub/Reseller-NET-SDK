namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class CreateOrderFromProductRequest : CreateOrderFromWizardRequest
    {
        public StandardPriceRequest Product { get; set; }
    }
}
