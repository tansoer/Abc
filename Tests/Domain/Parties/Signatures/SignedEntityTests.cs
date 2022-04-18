using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Signatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Signatures {

    [TestClass]
    public class SignedEntityTests
        : SealedTests<SignedEntity<BodyMetricString, BodyMetricData>, Entity<BodyMetricData>> {

        protected override SignedEntity<BodyMetricString, BodyMetricData> createObject() {
            base.createObject();
            var d = GetRandom.ObjectOf<BodyMetricData>();
            d.MetricType = MetricType.String;
            d.UnitId = null;
            d.MetricType = MetricType.String;
            return new SignedEntity<BodyMetricString, BodyMetricData>(d);
        }

        [TestMethod]
        public void EntityTest() {
            var e = obj.Entity;
            Assert.IsNotNull(e);
            Assert.IsInstanceOfType(e, typeof(BodyMetricString));
            allPropertiesAreEqual(e.Data, obj.Data);
        }
        [TestMethod]
        public void EntityIsQuantityMetricTest() {
            var d = GetRandom.ObjectOf<BodyMetricData>();
            d.MetricType = MetricType.Quantity;
            var o = new SignedEntity<BodyMetricQuantity, BodyMetricData>(d);
            var e = o.Entity;
            Assert.IsNotNull(e);
            Assert.IsInstanceOfType(e, typeof(BodyMetricQuantity));
            allPropertiesAreEqual(e.Data, o.Data);
        }
    }

}