using System;
using System.Collections.Generic;
using System.Net.Http;
using CMYKhub.ResellerApi.Client.Manufacturing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace CMYKhub.ResellerApi.Client.Tests
{
    [TestClass]
    public class When_get_product : HublinkManufacturingClientTests
    {
        private const string productId = "268";
        private Product result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Products?productid=268")
                .Respond("application/json", "Resources.Product_268.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetProductAsync(productId).Result;
        }

        [TestMethod]
        public void Should_return_product()
        {
            Assert.IsNotNull(result);
        }
    }


    [TestClass]
    public class When_get_products : HublinkManufacturingClientTests
    {
        private IEnumerable<Product> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Products")
                .Respond("application/json", "Resources.Products.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetProductsAsync().Result;
        }

        [TestMethod]
        public void Should_return_products()
        {
            Assert.IsNotNull(result);
        }
    }
    [TestClass]
    public class When_get_products_by_name : HublinkManufacturingClientTests
    {
        private IEnumerable<Product> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Products?name=A4")
                .Respond("application/json", "Resources.Products_A4.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetProductsByNameAsync("A4").Result;
        }

        [TestMethod]
        public void Should_return_products()
        {
            Assert.IsNotNull(result);
        }
    }
}
