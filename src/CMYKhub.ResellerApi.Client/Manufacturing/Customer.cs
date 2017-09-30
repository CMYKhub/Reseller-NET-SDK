using System.Collections.Generic;

namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class Customer : ApiResource
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public Address Address { get; set; }
        public IDictionary<string, string> ContactMethods { get; set; }
    }
}
