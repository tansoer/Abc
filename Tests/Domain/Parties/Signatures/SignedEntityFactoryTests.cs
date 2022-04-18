using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Signatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Signatures {
    [TestClass]
    public class SignedEntityFactoryTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(SignedEntityFactory);

        [TestMethod] public void CreateTest() => Assert.IsNull(SignedEntityFactory.Create(GetRandom.ObjectOf<SignedEntityTypeData>()));

        [TestMethod]
        public void CreateBodyMetricStringTest() {
            var d = GetRandom.ObjectOf<BodyMetricData>();
            d.MetricType = MetricType.String;
            d.UnitId = null;
            var o = SignedEntityFactory.Create(d) as BodyMetricString;
            Assert.IsNotNull(o);
            allPropertiesAreEqual(d, o.Data);
        }

        [TestMethod]
        public void CreateBodyMetricQuantityTest() {
            var d = GetRandom.ObjectOf<BodyMetricData>();
            d.MetricType = MetricType.Quantity;
            var o = SignedEntityFactory.Create(d) as BodyMetricQuantity;
            Assert.IsNotNull(o);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}
