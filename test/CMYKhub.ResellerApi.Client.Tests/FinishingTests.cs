using System;
using System.Collections.Generic;
using System.Net.Http;
using CMYKhub.ResellerApi.Client.Manufacturing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace CMYKhub.ResellerApi.Client.Tests
{
    [TestClass]
    public class When_get_finishing : HublinkManufacturingClientTests
    {
        private const string finishingId = "85";
        private Finishing result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Finishings?finishingid=85")
                .Respond("application/json", "Resources.Finishing_85.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetFinishingAsync(finishingId).Result;
        }

        [TestMethod]
        public void Should_return_finishing()
        {
            Assert.IsNotNull(result);
        }
    }


    [TestClass]
    public class When_get_finishings : HublinkManufacturingClientTests
    {
        private IEnumerable<Finishing> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Finishings")
                .Respond("application/json", "Resources.Finishings.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetFinishingsAsync().Result;
        }

        [TestMethod]
        public void Should_return_finishings()
        {
            Assert.IsNotNull(result);
        }
    }
    [TestClass]
    public class When_get_finishings_by_name : HublinkManufacturingClientTests
    {
        private IEnumerable<Finishing> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Finishings?name=fold")
                .Respond("application/json", "Resources.Finishings_fold.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetFinishingsByNameAsync("fold").Result;
        }

        [TestMethod]
        public void Should_return_finishings()
        {
            Assert.IsNotNull(result);
        }
    }

    [TestClass]
    public class When_get_finishings_by_available_for_flat_sheet_spec : HublinkManufacturingClientTests
    {
        private FinishingAvailableSpec spec = new FinishingAvailableSpec
        {
            Width = 210,
            Height = 297,
            PaperWeight = 170,
            PrintType = PrintTypes.Offset
        };
        private IEnumerable<Finishing> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Finishing/Available?width=210&height=297&paperWeight=170&printType=1")
                .Respond("application/json", "Resources.Finishings_available_spec1.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetFinishingsByAvailableAsync(spec).Result;
        }

        [TestMethod]
        public void Should_return_finishings()
        {
            Assert.IsNotNull(result);
        }
    }

    [TestClass]
    public class When_get_finishings_by_available_for_booklet_spec : HublinkManufacturingClientTests
    {
        private FinishingAvailableSpec spec = new FinishingAvailableSpec
        {
            Width = 210,
            Height = 297,
            PaperWeight = 300,
            PrintType = PrintTypes.Offset,
            Book = new FinishingAvailableBookletSpec { Pp = 32, Orientation = Orientations.Portrait }
        };
        private IEnumerable<Finishing> result;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            mockHttp.When(HttpMethod.Get, $"{baseUri}/man/Finishing/Available?width=210&height=297&paperWeight=300&printType=1&book.pp=32&book.orientation=0")
                .Respond("application/json", "Resources.Finishings_available_spec2.json".ReadStringResource());

            Act();
        }

        public override void Act()
        {
            result = client.GetFinishingsByAvailableAsync(spec).Result;
        }

        [TestMethod]
        public void Should_return_finishings()
        {
            Assert.IsNotNull(result);
        }
    }
}
