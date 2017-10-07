﻿using System.Linq;
using System.Net.Http;
using CMYKhub.ResellerApi.Client.Manufacturing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace CMYKhub.ResellerApi.Client.Tests
{
    [TestClass]
    public class When_create_price_for_standard_product : HublinkManufacturingClientTests
    {
        private StandardPriceRequest request = new StandardPriceRequest
        {
            FinishedSize = new Size { Width = 290, Height = 400 },
            Kinds = 2,
            ProductId = "268",
            Quantity = 1000
        };


        private ProductPrice result;

        private bool Compare(StandardPriceRequest left, StandardPriceRequest right)
        {
            return left.DeliveryType == right.DeliveryType &&
                   left.FinishedSize.Width == right.FinishedSize.Width &&
                   left.FinishedSize.Height == right.FinishedSize.Height &&
                   left.Finishing.Length == right.Finishing.Length &&
                   left.Finishing.All(x => right.Finishing.Any(y => y.FinishingId == x.FinishingId && y.NoItems == x.NoItems)) &&
                   left.FreightProviderId == right.FreightProviderId &&
                   left.Kinds == right.Kinds &&
                   left.ProductId == right.ProductId &&
                   left.Quantity == right.Quantity;
        }

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/man/Pricing/Standard")
                .With(r => Compare(request, r.Content.ReadAsAsync<StandardPriceRequest>().Result))
                .Respond("application/json", "Resources.Pricing_Standard_1.json".ReadStringResource());

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
            Assert.AreEqual(691.4m, result.Price.ExTax);
        }
    }

    [TestClass]
    public class When_create_price_for_standard_product_with_finishing : HublinkManufacturingClientTests
    {
        private StandardPriceRequest request = new StandardPriceRequest
        {
            ProductId = "206",
            Quantity = 1000,
            Kinds = 2,
            FinishedSize = new Size { Width = 205, Height = 280 },
            Finishing = new[] { new PrintWizardFinishing { FinishingId = "2", NoItems = 500 } }
        };

        private ProductPrice result;

        private bool Compare(StandardPriceRequest left, StandardPriceRequest right)
        {
            return left.DeliveryType == right.DeliveryType &&
                   left.FinishedSize.Width == right.FinishedSize.Width &&
                   left.FinishedSize.Height == right.FinishedSize.Height &&
                   left.Finishing.Length == right.Finishing.Length &&
                   left.Finishing.All(x => right.Finishing.Any(y => y.FinishingId == x.FinishingId && y.NoItems == x.NoItems)) &&
                   left.FreightProviderId == right.FreightProviderId &&
                   left.Kinds == right.Kinds &&
                   left.ProductId == right.ProductId &&
                   left.Quantity == right.Quantity;
        }

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/man/Pricing/Standard")
                .With(r => Compare(request, r.Content.ReadAsAsync<StandardPriceRequest>().Result))
                .Respond("application/json", "Resources.Pricing_Standard_2.json".ReadStringResource());

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
            Assert.AreEqual(338.5m, result.Price.ExTax);
        }
    }
}
