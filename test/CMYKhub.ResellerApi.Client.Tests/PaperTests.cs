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
    public class When_get_paper : HublinkManufacturingClientTests
    {
        private const string paperId = "356";
        private Paper result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Papers?paperid=356")
                .Respond("application/json", "Resources.Paper_356.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetPaperAsync(paperId).Result;
        }

        [TestMethod]
        public void Should_return_paper()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Id_should_match_value_in_response()
        {
            Assert.AreEqual(paperId, result.Id);
        }

        [TestMethod]
        public void Description_should_match_value_in_response()
        {
            Assert.AreEqual("EcoStar Uncoated 100% Recycled", result.Description);
        }

        [TestMethod]
        public void Family_should_match_value_in_response()
        {
            Assert.AreEqual("EcoStar Uncoated 100% Recycled", result.Family);
        }

        [TestMethod]
        public void Colour_should_match_value_in_response()
        {
            Assert.AreEqual("White", result.Colour);
        }

        [TestMethod]
        public void Texture_should_match_value_in_response()
        {
            Assert.AreEqual("Uncoated", result.Texture);
        }

        [TestMethod]
        public void Weight_should_match_value_in_response()
        {
            Assert.AreEqual(100, result.Weight);
        }
    }


    [TestClass]
    public class When_get_papers : HublinkManufacturingClientTests
    {
        private IEnumerable<Paper> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Papers")
                .Respond("application/json", "Resources.Papers.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetPapersAsync().Result;
        }

        [TestMethod]
        public void Should_return_papers()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Should_return_papers_with_same_count_as_response()
        {
            Assert.AreEqual(67, result.Count());
        }

        [TestMethod]
        public void Id_should_match_value_in_response()
        {
            Assert.AreEqual("1987", result.ElementAt(63).Id);
        }

        [TestMethod]
        public void Description_should_match_value_in_response()
        {
            Assert.AreEqual("Synthetic Clear Self Adhesive", result.ElementAt(63).Description);
        }
    }

    [TestClass]
    public class When_get_papers_by_name : HublinkManufacturingClientTests
    {
        private IEnumerable<Paper> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Papers?name=uncoated")
                .Respond("application/json", "Resources.Papers_uncoated.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetPapersByNameAsync("uncoated").Result;
        }

        [TestMethod]
        public void Should_return_papers()
        {
            Assert.IsNotNull(result);
        }
    }
}
