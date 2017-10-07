using CMYKhub.ResellerApi.Client.Manufacturing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CMYKhub.ResellerApi.Client.Tests
{
    [TestClass]
    public abstract class PricingTests<T> : HublinkManufacturingClientTests where T : PriceRequest
    {
        protected abstract bool Compare(T left, T right);
    }
}
