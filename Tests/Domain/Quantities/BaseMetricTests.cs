using Abc.Aids.Constants;
using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {

    [TestClass]
    public class BaseMetricTests :AbstractTests<BaseMetric<MeasureData>, Entity<MeasureData>> {

        private class testClass :BaseMetric<MeasureData> {
            public testClass(MeasureData d = null) : base(d) { }
        }

        protected override BaseMetric<MeasureData> createObject()
            => new testClass(GetRandom.ObjectOf<MeasureData>());

        [TestMethod]
        public void CodeTest() {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.Code = null;
            var x = new testClass(d);
            Assert.AreEqual(x.Name, x.Code);
            Assert.AreEqual(d.Name, x.Name);
            Assert.AreEqual(d.Id, x.Id);
        }

        [TestMethod]
        public void NameTest() {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.Code = null;
            d.Name = null;
            var x = new testClass(d);
            Assert.AreEqual(x.Id, x.Code);
            Assert.AreEqual(x.Id, x.Name);
            Assert.AreEqual(d.Id, x.Id);
        }
        [TestMethod]
        public void IdTest() {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.Code = null;
            d.Name = null;
            d.Id = null;
            var x = new testClass(d);
            Assert.AreEqual(Word.Unspecified, x.Code);
            Assert.AreEqual(Word.Unspecified, x.Name);
            Assert.AreEqual(Word.Unspecified, x.Id);
        }
    }
}
