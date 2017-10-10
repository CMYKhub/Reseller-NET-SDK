using System;
using System.Linq;
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
            return BookletPriceComparer.Compare(left, right);
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

        [TestMethod]
        public void Tax_should_match_value_in_response()
        {
            Assert.AreEqual(196.66m, result.Price.Tax);
        }

        [TestMethod]
        public void Total_should_match_value_in_response()
        {
            Assert.AreEqual(2163.22m, result.Price.IncTax);
        }

        [TestMethod]
        public void CurrencyCode_should_match_value_in_response()
        {
            Assert.AreEqual("AUD", result.Price.Currency.Code);
        }

        [TestMethod]
        public void Expiry_should_match_value_in_response()
        {
            Assert.AreEqual(new DateTime(2017, 01, 01, 0, 0, 0, DateTimeKind.Utc), result.Expires);
        }

        [TestMethod]
        public void ResellerId_should_match_value_in_response()
        {
            Assert.AreEqual("4926", result.ResellerId);
        }

        [TestMethod]
        public void Token_should_match_value_in_response()
        {
            Assert.AreEqual("eyIkdHlwZSI6IkNNWiLkNvcmUuUdmljZXMiLCJXaXphcmQiOnsiJrtg", result.Token);
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
