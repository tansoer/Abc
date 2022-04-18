using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;
using Abc.Infra.Quantities;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Common {

    [TestClass]
    public class FilteredRepoTests : AbstractTests<FilteredRepo<Measure, MeasureData>,
        CrudRepo<Measure, MeasureData>> {
        private class testClass : FilteredRepo<Measure, MeasureData> {
            public testClass(DbContext c, DbSet<MeasureData> s) : base(c, s) { }
            protected internal override Measure toDomainObject(MeasureData d) => MeasureFactory.Create(d);
        }
        protected override FilteredRepo<Measure, MeasureData> createObject() {
            var options = new DbContextOptionsBuilder<QuantityDb>().UseInMemoryDatabase("TestDb").Options;
            var c = new QuantityDb(options);
            var o = new testClass(c, c.Measures);

            for (var i = 0; i < GetRandom.UInt8(10, 30); i++) {
                var d = GetRandom.ObjectOf<MeasureData>();
                o.Add(MeasureFactory.Create(d));
            }
            return o;
        }
        [TestMethod] public void SearchStringTest() => isNullable<string>();
        [TestMethod] public void CurrentFilterTest() => isNullable<string>();
        [TestMethod] public void FixedFilterTest() => isNullable<string>();
        [TestMethod] public void FixedValueTest() => isNullable<string>();
        [TestMethod] public void FixedValuesTest() => isNullable<string[]>();
        [TestMethod] public void GetTest() {
            var l = obj.Get();
            Assert.AreEqual(obj.dbSet.Count(), l.Count);
        }
        [TestMethod] public void GetFilteredListTest() {
            var s = rndStr;
            var c = GetRandom.UInt8(10, 30);
            for (var i = 0; i < c; i++) {
                var d = GetRandom.ObjectOf<MeasureData>();
                addFilter(d, s, i);
                obj.Add(MeasureFactory.Create(d));
            }
            var l = obj.Get();
            Assert.AreEqual(obj.dbSet.Count(), l.Count);
            obj.SearchString = s;
            l = obj.Get();
            Assert.AreEqual(c, l.Count);
        }
        private static void addFilter(EntityBaseData d, string s, int i) {
            var x = i % 3;
            if (x == 0) d.Id += s;
            else if (x == 1) d.Name += s;
            else if (x == 2) d.Code += s;
        }
        [TestMethod] public void GetFixedListTest() {
            var s1 = GetRandom.String(10, 20);
            var s2 = GetRandom.String(10, 20);
            var c1 = GetRandom.UInt8(10, 30);
            var c2 = GetRandom.UInt8(10, 30);
            var c3 = GetRandom.UInt8(10, 30);

            for (var i = 0; i < c1; i++) {
                var d = GetRandom.ObjectOf<MeasureData>();
                addFilter(d, s1, i);
                obj.Add(MeasureFactory.Create(d));
            }

            for (var i = 0; i < c2; i++) {
                var d = GetRandom.ObjectOf<MeasureData>();
                addFilter(d, s1, i);
                d.Details = s2;
                obj.Add(MeasureFactory.Create(d));
            }

            for (var i = 0; i < c3; i++) {
                var d = GetRandom.ObjectOf<MeasureData>();
                d.Details = s2;
                obj.Add(MeasureFactory.Create(d));
            }

            var l = obj.Get();
            Assert.AreEqual(obj.dbSet.Count(), l.Count);
            obj.SearchString = s1;
            l = obj.Get();
            Assert.AreEqual(c1 + c2, l.Count);
            obj.FixedFilter = "Details";
            obj.FixedValue = s2;
            l = obj.Get();
            Assert.AreEqual(c2, l.Count);
            obj.SearchString = null;
            l = obj.Get();
            Assert.AreEqual(c2 + c3, l.Count);
        }
        [TestMethod]
        public void GetFixedValuesListTest() {
            for (int i = 0; i < GetRandom.Int32(1, 300); i++) {
                var d = GetRandom.ObjectOf<MeasureData>();
                obj.Add(MeasureFactory.Create(d));
            }
            var l = obj.Get();
            var c = GetRandom.Int32(1, l.Count);
            var fixedValues = new string[c];
            for (int i = 0; i < c; i++) { fixedValues[i] = l[i].Id; }
            obj.FixedFilter = "Id";
            obj.FixedValue = "partyId";
            obj.FixedValues = fixedValues;
            l = obj.Get();
            Assert.AreEqual(c, l.Count);
        }
        [TestMethod] public void GetFilteredTest() {
            var count = GetRandom.UInt8(3, 7);
            var value = rndStr;
            for (var i = 0; i < 3*count; i++) {
                var d = GetRandom.ObjectOf<MeasureData>();
                if (i % 3 == 0) d.Code = value;
                obj.Add(MeasureFactory.Create(d));
            }
            var l = obj.Get("Code", value);
            Assert.AreEqual(count, l.Count);
        } 
    }
}

