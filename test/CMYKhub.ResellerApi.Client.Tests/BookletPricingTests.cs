﻿using System.Linq;
using System.Net.Http;
using CMYKhub.ResellerApi.Client.Manufacturing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace CMYKhub.ResellerApi.Client.Tests
{
    [TestClass]
    public abstract class BookletPricingTests : PricingTests<BookletProductRequest>
    {
        protected override bool Compare(BookletProductRequest left, BookletProductRequest right)
        {
            return left.DeliveryType == right.DeliveryType &&
                   left.FinishedSize.Width == right.FinishedSize.Width &&
                   left.FinishedSize.Height == right.FinishedSize.Height &&

                   left.Body.PaperId == right.Body.PaperId &&
                   left.Body.Pp == right.Body.Pp &&

                   CompareCover(left.Cover, right.Cover) &&

                   left.FreightProviderId == right.FreightProviderId &&
                   left.Orientation == right.Orientation &&
                   left.PrintType == right.PrintType &&
                   left.Quantity == right.Quantity;
        }

        private bool CompareCover(BookletCoverSection left, BookletCoverSection right)
        {
            if (left == null && right == null) return true;
            if (left == null) return false;
            if (right == null) return false;


            return left.PaperId == right.PaperId &&
                   left.Pp == right.Pp &&
                   left.ProductId == right.ProductId &&
                   left.Finishing.Length == right.Finishing.Length &&
                   left.Finishing.All(x => right.Finishing.Any(y => y.FinishingId == x.FinishingId && y.NoItems == x.NoItems));
        }
    }

    [TestClass]
    public class When_create_price_for_booklet_with_custom_cover : BookletPricingTests
    {
        private BookletProductRequest request = new BookletProductRequest
        {
            BindingId = "1",
            Quantity = 250,
            Orientation = 0,
            FinishedSize = new Size { Width = 297, Height = 210 },
            PrintType = 1,
            Body = new BookletBodySection
            {
                PaperId = "151",
                Pp = 32
            },
            Cover = new BookletCoverSection
            {
                PaperId = "160",
                Pp = 4,
                Finishing = new[] { new PrintWizardFinishing { FinishingId = "2", NoItems = 500 } }
            }
        };
        private ProductPrice result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/man/Pricing/Booklet")
                .With(r => Compare(request, r.Content.ReadAsAsync<BookletProductRequest>().Result))
                .Respond("application/json", "Resources.Pricing_Booklet_CustomCover.json".ReadStringResource());

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
            Assert.AreEqual(1966.56m, result.Price.ExTax);
        }
    }

    [TestClass]
    public class When_create_price_for_booklet_with_product_cover : BookletPricingTests
    {
        private BookletProductRequest request = new BookletProductRequest
        {
            BindingId = "1",
            Quantity = 250,
            Orientation = 0,
            FinishedSize = new Size { Width = 297, Height = 210 },
            PrintType = 2,
            Body = new BookletBodySection
            {
                PaperId = "215",
                Pp = 32
            },
            Cover = new BookletCoverSection
            {
                ProductId = "1400"
            }
        };
        private ProductPrice result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/man/Pricing/Booklet")
                .With(r => Compare(request, r.Content.ReadAsAsync<BookletProductRequest>().Result))
                .Respond("application/json", "Resources.Pricing_Booklet_ProductCover.json".ReadStringResource());

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
            Assert.AreEqual(1766.95m, result.Price.ExTax);
        }
    }

    [TestClass]
    public class When_create_price_for_booklet_with_self_cover : BookletPricingTests
    {
        private BookletProductRequest request = new BookletProductRequest
        {
            BindingId = "1",
            Quantity = 250,
            Orientation = 0,
            FinishedSize = new Size { Width = 297, Height = 210 },
            PrintType = 1,
            Body = new BookletBodySection
            {
                PaperId = "151",
                Pp = 32
            }
        };
        private ProductPrice result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/man/Pricing/Booklet")
                .With(r => Compare(request, r.Content.ReadAsAsync<BookletProductRequest>().Result))
                .Respond("application/json", "Resources.Pricing_Booklet_SelfCover.json".ReadStringResource());

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
            Assert.AreEqual(1637.38m, result.Price.ExTax);
        }
    }
}