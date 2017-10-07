using System;

namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class ProductPrice
    {
        public DateTime Expires { get; set; }
        public Money Price { get; set; }
        public string ResellerId { get; set; }
        public string Token { get; set; }
    }
}
