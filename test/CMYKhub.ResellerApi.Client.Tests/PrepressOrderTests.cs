using System;
using System.Collections.Generic;
using System.Net.Http;
using CMYKhub.ResellerApi.Client.Prepress;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace CMYKhub.ResellerApi.Client.Tests
{
    [TestClass]
    public class When_get_prepress_order : HublinkPrepressClientTests
    {
        private const string orderId = "872411";
        private OrderResource result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/pp/Orders?orderid={orderId}")
                .Respond("application/json", $"Resources.Prepress_Order_{orderId}.json".ReadStringResource());

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
        public void DateOrdered_should_match_value_in_response()
        {
            Assert.AreEqual("a242f8ae-9d7d-4266-a352-e15df0b9bd62", result.PrepressDepartmentId);
        }

        [TestMethod]
        public void Status_Id_should_match_value_in_response()
        {
            Assert.AreEqual("1", result.Status.Id);
        }

        [TestMethod]
        public void Status_Name_should_match_value_in_response()
        {
            Assert.AreEqual("PendingPreflight", result.Status.Name);
        }

        [TestMethod]
        public void Files_should_match_value_in_response()
        {
            Assert.AreEqual(1, result.Files.Length);
        }

        [TestMethod]
        public void Files_ContentType_should_match_value_in_response()
        {
            Assert.AreEqual("application/pdf", result.Files[0].ContentType);
        }

        [TestMethod]
        public void Files_UploadedBy_should_match_value_in_response()
        {
            Assert.AreEqual("Joe Bloggs", result.Files[0].UploadedBy);
        }

        [TestMethod]
        public void Files_Filename_should_match_value_in_response()
        {
            Assert.AreEqual("User-Manual.pdf", result.Files[0].Filename);
        }

        [TestMethod]
        public void Files_UploadDate_should_match_value_in_response()
        {
            Assert.AreEqual(DateTimeOffset.Parse("2017-02-10T04:42:33.288Z", null, System.Globalization.DateTimeStyles.AssumeUniversal).DateTime, result.Files[0].UploadDate);
        }
    }

}
