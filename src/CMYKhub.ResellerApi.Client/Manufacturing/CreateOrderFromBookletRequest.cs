namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class CreateOrderFromBookletRequest : CreateOrderFromWizardRequest
    {
        public BookletProductRequest Booklet { get; set; }
    }
}
