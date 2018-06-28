using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CMYKhub.ResellerApi.Client.Manufacturing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace CMYKhub.ResellerApi.Client.Tests
{
    [TestClass]
    public abstract class WideFormatPricingTests : PricingTests<WideFormatPriceRequest>
    {
        protected override bool Compare(WideFormatPriceRequest left, WideFormatPriceRequest right)
        {
            return WideFormatPriceRequestComparer.Compare(left, right);
        }
    }

    [TestClass]
    public class When_create_price_for_wideFormat_product : WideFormatPricingTests
    {
        private WideFormatPriceRequest request = new WideFormatPriceRequest
        {
            FinishedSize = new Size { Width = 950, Height = 1400 },
            Kinds = 2,
            ProductId = "bd69fd35-7860-44b7-9099-e4c9e4fc6aa8",
            Quantity = 3,
            FreightProviderId = "55",
            DeliveryType = 1
        };
        private ProductPrice result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/man/Pricing/WideFormat")
                .With(r => Compare(request, r.Content.ReadAsAsync<WideFormatPriceRequest>().Result))
                .Respond("application/json", "Resources.Pricing_WideFormat_1.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.CreatePriceAsync(request).Result;
        }

        [TestMethod]
        public void Should_return_price()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Price_should_match_value_in_response()
        {
            Assert.AreEqual(105.61m, result.Price.ExTax);
        }
    }

    [TestClass]
    public class When_create_price_for_wideFormat_product_with_finishing : WideFormatPricingTests
    {
        private WideFormatPriceRequest request = new WideFormatPriceRequest
        {
            ProductId = "bd69fd35-7860-44b7-9099-e4c9e4fc6aa8",
            Quantity = 3,
            Kinds = 2,
            FinishedSize = new Size { Width = 900, Height = 1200 },
            Finishing = new[] { new WideFormatWizardFinishing { FinishingId = "2", Config = new Dictionary<string, string> { { "Spacing", "30cm" } } } }
        };
        private ProductPrice result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/man/Pricing/WideFormat")
                .With(r => Compare(request, r.Content.ReadAsAsync<WideFormatPriceRequest>().Result))
                .Respond("application/json", "Resources.Pricing_WideFormat_2.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.CreatePriceAsync(request).Result;
        }

        [TestMethod]
        public void Should_return_price()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Price_should_match_value_in_response()
        {
            Assert.AreEqual(77.22m, result.Price.ExTax);
        }
    }
}
