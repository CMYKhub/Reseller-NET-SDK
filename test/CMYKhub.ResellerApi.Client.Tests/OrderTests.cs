using System;
using System.Collections.Generic;
using System.Net.Http;
using CMYKhub.ResellerApi.Client.Manufacturing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace CMYKhub.ResellerApi.Client.Tests
{
    [TestClass]
    public class When_get_order : HublinkManufacturingClientTests
    {
        private const string orderId = "139424";
        private Order result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/orders?orderid=139424")
                .Respond("application/json", "Resources.Order_139424.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetOrderAsync(orderId).Result;
        }

        [TestMethod]
        public void Should_return_order()
        {
            Assert.IsNotNull(result);
        }
    }


    [TestClass]
    public class When_get_orders : HublinkManufacturingClientTests
    {
        private IEnumerable<Order> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/orders")
                .Respond("application/json", "Resources.Orders.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetOrdersAsync().Result;
        }

        [TestMethod]
        public void Should_return_orders()
        {
            Assert.IsNotNull(result);
        }
    }
}
