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
    public class When_get_wideFormatProduct : HublinkManufacturingClientTests
    {
        private const string wideFormatProductId = "bd69fd35-7860-44b7-9099-e4c9e4fc6aa8";
        private Product result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/WideFormat/Products?productid=bd69fd35-7860-44b7-9099-e4c9e4fc6aa8")
                .Respond("application/json", "Resources.WideFormatProduct_bd69fd35-7860-44b7-9099-e4c9e4fc6aa8.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetWideFormatProductAsync(wideFormatProductId).Result;
        }

        [TestMethod]
        public void Should_return_wideFormatProduct()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Id_should_match_value_in_response()
        {
            Assert.AreEqual(wideFormatProductId, result.Id);
        }

        [TestMethod]
        public void Name_should_match_value_in_response()
        {
            Assert.AreEqual("Canvas Poster 1 Side", result.Name);
        }

        [TestMethod]
        public void Description_should_match_value_in_response()
        {
            Assert.AreEqual("Canvas Poster 1 Side", result.Description);
        }

        [TestMethod]
        public void Code_should_match_value_in_response()
        {
            Assert.AreEqual("bd69fd35-7860-44b7-9099-e4c9e4fc6aa8", result.Code);
        }

        [TestMethod]
        public void ProductGroup_should_match_value_in_response()
        {
            Assert.IsNull( result.ProductGroup);
        }
    }


    [TestClass]
    public class When_get_wideFormatProducts : HublinkManufacturingClientTests
    {
        private IEnumerable<Product> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/WideFormat/Products")
                .Respond("application/json", "Resources.WideFormatProducts.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetWideFormatProductsAsync().Result;
        }

        [TestMethod]
        public void Should_return_wideFormatProducts()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Should_return_wideFormatProducts_with_same_count_as_in_response()
        {
            Assert.AreEqual(9, result.Count());
        }

        [TestMethod]
        public void Id_at_index_6_should_match_value_in_response()
        {
            Assert.AreEqual("a65eebf1-ab78-4450-a942-7ec21340ca8b", result.ElementAt(6).Id);
        }

        [TestMethod]
        public void Name_at_index_6_should_match_value_in_response()
        {
            Assert.AreEqual("Outdoor Polyester", result.ElementAt(6).Name);
        }

        [TestMethod]
        public void Description_at_index_6_should_match_value_in_response()
        {
            Assert.AreEqual("Outdoor Polyester", result.ElementAt(6).Description);
        }

        [TestMethod]
        public void Code_at_index_6_should_match_value_in_response()
        {
            Assert.AreEqual("a65eebf1-ab78-4450-a942-7ec21340ca8b", result.ElementAt(6).Code);
        }

        [TestMethod]
        public void ProductGroup_at_index_6_should_match_value_in_response()
        {
            Assert.IsNull(result.ElementAt(6).ProductGroup);
        }
    }

    [TestClass]
    public class When_get_wideFormatProducts_by_name : HublinkManufacturingClientTests
    {
        private IEnumerable<Product> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/WideFormat/Products?name=Canvas")
                .Respond("application/json", "Resources.WideFormatProducts_Canvas.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetWideFormatProductsByNameAsync("Canvas").Result;
        }

        [TestMethod]
        public void Should_return_wideFormatProducts()
        {
            Assert.IsNotNull(result);
        }
    }
}
