using System.Collections.Generic;

namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    /// <summary>
    /// Represents a customer of the reseller
    /// </summary>
    public class Customer : ApiResource
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public Address Address { get; set; }
        public IDictionary<string, string> ContactMethods { get; set; }
    }
}
