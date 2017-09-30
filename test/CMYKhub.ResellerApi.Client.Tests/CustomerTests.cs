using System;
using System.Collections.Generic;
using System.Net.Http;
using CMYKhub.ResellerApi.Client.Manufacturing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace CMYKhub.ResellerApi.Client.Tests
{
    [TestClass]
    public class When_get_customer : HublinkManufacturingClientTests
    {
        private const string customerId = "4306";
        private Customer result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Customers?customerid=4306")
                .Respond("application/json", "Resources.Customer_4306.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetCustomerAsync(customerId).Result;
        }

        [TestMethod]
        public void Should_return_customer()
        {
            Assert.IsNotNull(result);
        }
    }


    [TestClass]
    public class When_get_customers : HublinkManufacturingClientTests
    {
        private IEnumerable<Customer> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Customers")
                .Respond("application/json", "Resources.Customers.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetCustomersAsync().Result;
        }

        [TestMethod]
        public void Should_return_customers()
        {
            Assert.IsNotNull(result);
        }
    }
}
