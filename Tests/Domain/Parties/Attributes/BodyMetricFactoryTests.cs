using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Attributes {
    [TestClass]
    public class BodyMetricFactoryTests : TestsBase {

        private BodyMetricData d;

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(BodyMetricFactory);
            d = GetRandom.ObjectOf<BodyMetricData>();
        }

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateBodyMetricStringTest() {
            d.MetricType = MetricType.String;
            var o = BodyMetricFactory.Create(d);
            isInstanceOfType(o, typeof(BodyMetricString));
            allPropertiesAreEqual(d, o.Data);
        }

        [TestMethod]
        public void CreateBodyMetricQuantityTest() {
            d.MetricType = MetricType.Quantity;
            var o = BodyMetricFactory.Create(d);
            isInstanceOfType(o, typeof(BodyMetricQuantity));
            allPropertiesAreEqual(d, o.Data);
        }

        [TestMethod]
        public void CreateBodyMetricStringDataTest() {
            d.MetricType = MetricType.String;
            var o = BodyMetricFactory.Create(d);
            allPropertiesAreEqual(d, BodyMetricFactory.Create(o));
        }

        [TestMethod]
        public void CreateBodyMetricQuantityDataTest() {
            d.MetricType = MetricType.Quantity;
            var o = BodyMetricFactory.Create(d);
            allPropertiesAreEqual(d, BodyMetricFactory.Create(o));
        }
    }
}
