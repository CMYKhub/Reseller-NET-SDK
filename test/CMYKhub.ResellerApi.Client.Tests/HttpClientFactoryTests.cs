using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CMYKhub.ResellerApi.Client.Tests
{
    [TestClass]
    public class HttpClientFactoryTests
    {
        private HttpClientFactory sut;
        private HttpClient result;

        [TestInitialize]
        public virtual void Arrange()
        {
            sut = new HttpClientFactory();

            Act();
        }

        public virtual void Act()
        {
            result = sut.Create();
        }

        [TestMethod]
        public void Should_create_new_instance_of_httpClient()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Instance_created_should_not_be_singleton()
        {
            Assert.AreNotEqual(sut.Create(), result);
        }
    }
}
