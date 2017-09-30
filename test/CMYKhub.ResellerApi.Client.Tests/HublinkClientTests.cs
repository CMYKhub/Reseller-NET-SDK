using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RichardSzalay.MockHttp;

namespace CMYKhub.ResellerApi.Client.Tests
{
    [TestClass]
    public abstract class HublinkClientTests<T> where T : HublinkClient
    {
        protected Mock<IHttpClientFactory> clientFactory = new Mock<IHttpClientFactory>();
        protected const string baseUri = "http://tempuri.org", resellerId = "R-7345", apiKey = "KEY-123";
        protected readonly MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
        protected T client;

        protected abstract T CreateClient();

        [TestInitialize]
        public virtual void Arrange()
        {
            clientFactory.Setup(x => x.Create()).Returns(() => mockHttp.ToHttpClient());
            client = CreateClient();
            

            mockHttp.When(HttpMethod.Get, baseUri)
                .Respond("application/json", "Resources.Discovery_Base.json".ReadStringResource());
            mockHttp.When(HttpMethod.Get, $"{baseUri}/man")
                .Respond("application/json", "Resources.Discovery_Manufacturing.json".ReadStringResource());
            mockHttp.When(HttpMethod.Get, $"{baseUri}/pp")
                .Respond("application/json", "Resources.Discovery_Prepress.json".ReadStringResource());
        }

        public abstract void Act();
    }
}
