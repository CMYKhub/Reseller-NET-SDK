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
        private const string orderId = "872410";
        private Order result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/orders?orderid={orderId}")
                .Respond("application/json", $"Resources.Order_{orderId}.json".ReadStringResource());

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

        [TestMethod]
        public void OrderId_should_match_value_in_response()
        {
            Assert.AreEqual(orderId, result.OrderId);
        }

        [TestMethod]
        public void HubId_should_match_value_in_response()
        {
            Assert.AreEqual("1", result.HubId);
        }

        [TestMethod]
        public void DateOrdered_should_match_value_in_response()
        {
            Assert.AreEqual(DateTimeOffset.Parse("2017-02-09T08:05:51.764Z", null, System.Globalization.DateTimeStyles.AssumeUniversal).DateTime, result.DateOrdered);
        }

        [TestMethod]
        public void DateEstimatedDispatch_should_match_value_in_response()
        {
            Assert.AreEqual(DateTimeOffset.Parse("2017-10-16T04:00:00Z", null, System.Globalization.DateTimeStyles.AssumeUniversal).DateTime, result.DateEstimatedDispatch);
        }

        [TestMethod]
        public void Status_Id_should_match_value_in_response()
        {
            Assert.AreEqual("12", result.Status.Id);
        }

        [TestMethod]
        public void Status_Name_should_match_value_in_response()
        {
            Assert.AreEqual("Planning", result.Status.Name);
        }

        [TestMethod]
        public void OrderNumber_should_match_value_in_response()
        {
            Assert.AreEqual(orderId, result.OrderNumber);
        }

        [TestMethod]
        public void Quantity_should_match_value_in_response()
        {
            Assert.AreEqual(1000, result.Quantity);
        }

        [TestMethod]
        public void Description_should_match_value_in_response()
        {
            Assert.AreEqual("2 kinds of 170gsm Gloss Print 1 Side A4 Fold - Roll fold A4 to DL(500 items)", result.Description);
        }

        [TestMethod]
        public void ResellerId_should_match_value_in_response()
        {
            Assert.AreEqual("4926", result.ResellerId);
        }

        [TestMethod]
        public void Price_ExTax_should_match_value_in_response()
        {
            Assert.AreEqual(338.5m, result.Price.ExTax);
        }

        [TestMethod]
        public void Price_Tax_should_match_value_in_response()
        {
            Assert.AreEqual(33.85m, result.Price.Tax);
        }

        [TestMethod]
        public void Price_IncTax_should_match_value_in_response()
        {
            Assert.AreEqual(372.35m, result.Price.IncTax);
        }

        [TestMethod]
        public void Price_Currency_Code_should_match_value_in_response()
        {
            Assert.AreEqual("AUD", result.Price.Currency.Code);
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



    [TestClass]
    public class When_create_order_from_existing_quote : HublinkManufacturingClientTests
    {
        private CreateOrderFromQuoteRequest request = new CreateOrderFromQuoteRequest { QuoteId = "Q6345" };
        private CreatedOrderResource result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/man/orders")
                .With(r => r.Content.ReadAsAsync<CreateOrderFromQuoteRequest>().Result.QuoteId == request.QuoteId)
                .Respond("application/json", "Resources.Order_from_quote.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.CreateOrderAsync(request).Result;
        }

        [TestMethod]
        public void Should_return_order_created()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OrderId_should_match_value_in_response()
        {
            Assert.AreEqual("872416", result.OrderId);
        }

        [TestMethod]
        public void ResellerId_should_match_value_in_response()
        {
            Assert.AreEqual("4926", result.ResellerId);
        }
    }

    [TestClass]
    public class When_create_order_from_product : HublinkManufacturingClientTests
    {
        private CreateOrderFromProductRequest request = new CreateOrderFromProductRequest
        {
            Notes = "Please match colour B12",
            Reference = "REF: 123",
            Product = new StandardPriceRequest
            {
                FinishedSize = new Size { Width = 290, Height = 400 },
                Kinds = 2,
                ProductId = "268",
                Quantity = 1000
            }
        };
        private CreatedOrderResource result;

        private bool Compare(CreateOrderFromProductRequest left, CreateOrderFromProductRequest right)
        {
            return left.Notes == right.Notes &&
                   left.Reference == right.Reference &&
                   StandardPriceRequestComparer.Compare(left.Product, right.Product);
        }

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/man/orders")
                .With(r => Compare(request, r.Content.ReadAsAsync<CreateOrderFromProductRequest>().Result))
                .Respond("application/json", "Resources.Order_from_product.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.CreateOrderAsync(request).Result;
        }

        [TestMethod]
        public void Should_return_order_created()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OrderId_should_match_value_in_response()
        {
            Assert.AreEqual("872418", result.OrderId);
        }

        [TestMethod]
        public void ResellerId_should_match_value_in_response()
        {
            Assert.AreEqual("4926", result.ResellerId);
        }
    }

    [TestClass]
    public class When_create_order_from_booklet : HublinkManufacturingClientTests
    {
        private CreateOrderFromBookletRequest request = new CreateOrderFromBookletRequest
        {
            Notes = "Please match colour B12",
            Reference = "REF: 123",
            Booklet = new BookletProductRequest
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
            }
        };
        private CreatedOrderResource result;

        private bool Compare(CreateOrderFromBookletRequest left, CreateOrderFromBookletRequest right)
        {
            return left.Notes == right.Notes &&
                   left.Reference == right.Reference &&
                   BookletPriceComparer.Compare(left.Booklet, right.Booklet);
        }

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/man/orders")
                .With(r => Compare(request, r.Content.ReadAsAsync<CreateOrderFromBookletRequest>().Result))
                .Respond("application/json", "Resources.Order_from_booklet.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.CreateOrderAsync(request).Result;
        }

        [TestMethod]
        public void Should_return_order_created()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OrderId_should_match_value_in_response()
        {
            Assert.AreEqual("872417", result.OrderId);
        }

        [TestMethod]
        public void ResellerId_should_match_value_in_response()
        {
            Assert.AreEqual("4926", result.ResellerId);
        }
    }

    [TestClass]
    public class When_create_order_from_wide_format_product : HublinkManufacturingClientTests
    {
        private CreateOrderFromWideFormatRequest request = new CreateOrderFromWideFormatRequest
        {
            Notes = "Please match colour B12",
            Reference = "REF: 123",
            WideFormat = new WideFormatPriceRequest
            {
                FinishedSize = new Size { Width = 290, Height = 400 },
                Kinds = 2,
                ProductId = "268",
                Quantity = 1000
            }
        };
        private CreatedOrderResource result;

        private bool Compare(CreateOrderFromWideFormatRequest left, CreateOrderFromWideFormatRequest right)
        {
            return left.Notes == right.Notes &&
                   left.Reference == right.Reference &&
                   WideFormatPriceRequestComparer.Compare(left.WideFormat, right.WideFormat);
        }

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/man/orders")
                .With(r => Compare(request, r.Content.ReadAsAsync<CreateOrderFromWideFormatRequest>().Result))
                .Respond("application/json", "Resources.Order_from_product.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.CreateOrderAsync(request).Result;
        }

        [TestMethod]
        public void Should_return_order_created()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OrderId_should_match_value_in_response()
        {
            Assert.AreEqual("872418", result.OrderId);
        }

        [TestMethod]
        public void ResellerId_should_match_value_in_response()
        {
            Assert.AreEqual("4926", result.ResellerId);
        }
    }

    [TestClass]
    public class When_create_order_from_token : HublinkManufacturingClientTests
    {
        private CreateOrderFromTokenRequest request = new CreateOrderFromTokenRequest
        {
            Notes = "Please match colour B12",
            Reference = "REF: 123",
            Token = "SOME TOKEN HERE"
        };
        private CreatedOrderResource result;

        private bool Compare(CreateOrderFromTokenRequest left, CreateOrderFromTokenRequest right)
        {
            return left.Notes == right.Notes &&
                   left.Reference == right.Reference &&
                   left.Token == right.Token;
        }

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Post, $"{baseUri}/man/orders")
                .With(r => Compare(request, r.Content.ReadAsAsync<CreateOrderFromTokenRequest>().Result))
                .Respond("application/json", "Resources.Order_from_token.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.CreateOrderAsync(request).Result;
        }

        [TestMethod]
        public void Should_return_order_created()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OrderId_should_match_value_in_response()
        {
            Assert.AreEqual("872419", result.OrderId);
        }

        [TestMethod]
        public void ResellerId_should_match_value_in_response()
        {
            Assert.AreEqual("4926", result.ResellerId);
        }
    }
}
