using System;
using System.Collections.Generic;
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
