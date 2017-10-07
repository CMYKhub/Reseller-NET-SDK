namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class Money
    {
        public decimal ExTax { get; set; }
        public decimal Tax { get; set; }
        public decimal IncTax { get; set; }
        public Currency Currency { get; set; }
    }
}
