namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class CreateOrderFromQuoteRequest : CreateOrderRequest
    {
        public string QuoteId { get; set; }
    }
}
