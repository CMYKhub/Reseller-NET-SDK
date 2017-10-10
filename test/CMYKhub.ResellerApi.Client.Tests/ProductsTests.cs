using System;
using System.Collections.Generic;
using System.Linq;
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

        [TestMethod]
        public void Id_should_match_value_in_response()
        {
            Assert.AreEqual(productId, result.Id);
        }

        [TestMethod]
        public void Name_should_match_value_in_response()
        {
            Assert.AreEqual("A3", result.Name);
        }

        [TestMethod]
        public void Description_should_match_value_in_response()
        {
            Assert.AreEqual("150gsm Gloss Print 2 Side A3", result.Description);
        }

        [TestMethod]
        public void Code_should_match_value_in_response()
        {
            Assert.AreEqual("A-7723", result.Code);
        }

        [TestMethod]
        public void ProductGroup_Id_should_match_value_in_response()
        {
            Assert.AreEqual("35", result.ProductGroup.Id);
        }

        [TestMethod]
        public void ProductGroup_Name_should_match_value_in_response()
        {
            Assert.AreEqual("150gsm Gloss Print 2 Side", result.ProductGroup.Name);
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

        [TestMethod]
        public void Should_return_products_with_same_count_as_in_response()
        {
            Assert.AreEqual(362, result.Count());
        }

        [TestMethod]
        public void Id_at_index_165_should_match_value_in_response()
        {
            Assert.AreEqual("2169", result.ElementAt(165).Id);
        }

        [TestMethod]
        public void Name_at_index_165_should_match_value_in_response()
        {
            Assert.AreEqual("A1", result.ElementAt(165).Name);
        }

        [TestMethod]
        public void Description_at_index_165_should_match_value_in_response()
        {
            Assert.AreEqual("150gsm Ultra Gloss Print 1 Side A1", result.ElementAt(165).Description);
        }

        [TestMethod]
        public void Code_at_index_165_should_match_value_in_response()
        {
            Assert.AreEqual(null, result.ElementAt(165).Code);
        }

        [TestMethod]
        public void ProductGroup_Id_at_index_165_should_match_value_in_response()
        {
            Assert.AreEqual("214", result.ElementAt(165).ProductGroup.Id);
        }

        [TestMethod]
        public void ProductGroup_Name_at_index_165_should_match_value_in_response()
        {
            Assert.AreEqual("150gsm Ultra Gloss Print 1 Side", result.ElementAt(165).ProductGroup.Name);
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
