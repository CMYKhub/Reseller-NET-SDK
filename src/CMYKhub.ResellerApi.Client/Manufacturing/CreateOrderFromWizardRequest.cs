namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public abstract class CreateOrderFromWizardRequest : CreateOrderRequest
    {
        public string Reference { get; set; }
        public string Notes { get; set; }
    }
}
