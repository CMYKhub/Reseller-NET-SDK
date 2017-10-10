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
    public class When_get_customer : HublinkManufacturingClientTests
    {
        private const string customerId = "4306";
        private Customer result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Customers?customerid=4306")
                .Respond("application/json", "Resources.Customer_4306.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetCustomerAsync(customerId).Result;
        }

        [TestMethod]
        public void Should_return_customer()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CustomerId_should_match_value_in_response()
        {
            Assert.AreEqual("4306", result.CustomerId);
        }

        [TestMethod]
        public void Name_should_match_value_in_response()
        {
            Assert.AreEqual("Test Customer 4306", result.Name);
        }

        [TestMethod]
        public void ContactName_should_match_value_in_response()
        {
            Assert.AreEqual("Joe Bloggs", result.ContactName);
        }

        [TestMethod]
        public void Address_State_Name_should_match_value_in_response()
        {
            Assert.AreEqual("Victoria", result.Address.State.Name);
        }

        [TestMethod]
        public void Address_State_Abbreviation_should_match_value_in_response()
        {
            Assert.AreEqual("VIC", result.Address.State.Abbreviation);
        }

        [TestMethod]
        public void Address_State_CountryCode_should_match_value_in_response()
        {
            Assert.AreEqual("AU", result.Address.State.CountryCode);
        }

        [TestMethod]
        public void Address_Country_Code_should_match_value_in_response()
        {
            Assert.AreEqual("AU", result.Address.Country.Code);
        }

        [TestMethod]
        public void Address_Country_Name_should_match_value_in_response()
        {
            Assert.AreEqual("Australia", result.Address.Country.Name);
        }

        [TestMethod]
        public void Address_Address1_should_match_value_in_response()
        {
            Assert.AreEqual("test, t", result.Address.Address1);
        }

        [TestMethod]
        public void Address_Address2_should_match_value_in_response()
        {
            Assert.AreEqual("Level 1", result.Address.Address2);
        }

        [TestMethod]
        public void Address_City_should_match_value_in_response()
        {
            Assert.AreEqual("test", result.Address.City);
        }

        [TestMethod]
        public void Address_Postcode_should_match_value_in_response()
        {
            Assert.AreEqual("12345", result.Address.Postcode);
        }

        [TestMethod]
        public void ContactMethods_Email_should_match_value_in_response()
        {
            Assert.AreEqual("test321012@cmykhub.com", result.ContactMethods["Email"]);
        }

        [TestMethod]
        public void ContactMethods_Phone_should_match_value_in_response()
        {
            Assert.AreEqual("0435342212", result.ContactMethods["Phone"]);
        }
        
    }


    [TestClass]
    public class When_get_customers : HublinkManufacturingClientTests
    {
        private IEnumerable<Customer> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Customers")
                .Respond("application/json", "Resources.Customers.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetCustomersAsync().Result;
        }

        [TestMethod]
        public void Should_return_customers()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Should_have_same_number_of_customers_as_in_response()
        {
            Assert.AreEqual(10, result.Count());
        }

        [TestMethod]
        public void CustomerId_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("6518", result.ElementAt(9).CustomerId);
        }

        [TestMethod]
        public void Name_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("Test Customer 321012", result.ElementAt(9).Name);
        }

        [TestMethod]
        public void ContactName_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("test", result.ElementAt(9).ContactName);
        }

        [TestMethod]
        public void Address_State_Name_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("Victoria", result.ElementAt(9).Address.State.Name);
        }

        [TestMethod]
        public void Address_State_Abbreviation_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("VIC", result.ElementAt(9).Address.State.Abbreviation);
        }

        [TestMethod]
        public void Address_State_CountryCode_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("AU", result.ElementAt(9).Address.State.CountryCode);
        }

        [TestMethod]
        public void Address_Country_Code_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("AU", result.ElementAt(9).Address.Country.Code);
        }

        [TestMethod]
        public void Address_Country_Name_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("Australia", result.ElementAt(9).Address.Country.Name);
        }

        [TestMethod]
        public void Address_Address1_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("test, t", result.ElementAt(9).Address.Address1);
        }

        [TestMethod]
        public void Address_Address2_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("Level 1", result.ElementAt(9).Address.Address2);
        }

        [TestMethod]
        public void Address_City_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("test", result.ElementAt(9).Address.City);
        }

        [TestMethod]
        public void Address_Postcode_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("12345", result.ElementAt(9).Address.Postcode);
        }

        [TestMethod]
        public void ContactMethods_Email_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("test321012@cmykhub.com", result.ElementAt(9).ContactMethods["Email"]);
        }

        [TestMethod]
        public void ContactMethods_Phone_from_9th_index_should_match_value_in_response()
        {
            Assert.AreEqual("0435342212", result.ElementAt(9).ContactMethods["Phone"]);
        }
    }
}
