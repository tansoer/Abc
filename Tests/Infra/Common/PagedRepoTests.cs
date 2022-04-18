using System;
using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra;
using Abc.Infra.Common;
using Abc.Infra.Quantities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Common {

    [TestClass]
    public class PagedRepoTests : AbstractTests<PagedRepo<Measure, MeasureData>,
        OrderedRepo<Measure, MeasureData>> {
        private class testClass : PagedRepo<Measure, MeasureData> {
            public testClass(DbContext c, DbSet<MeasureData> s) : base(c, s) { }
            protected internal override Measure toDomainObject(MeasureData d) => MeasureFactory.Create(d);
        }

        protected override PagedRepo<Measure, MeasureData> createObject() {
            var options = new DbContextOptionsBuilder<QuantityDb>().UseInMemoryDatabase("TestDb").Options;
            var c = new QuantityDb(options);
            var o = new testClass(c, c.Measures);

            for (var i = 0; i < GetRandom.UInt8(10, 30); i++) {
                var d = GetRandom.ObjectOf<MeasureData>();
                o.Add(MeasureFactory.Create(d));
            }
            return o;
        }

        [TestMethod] public void PageIndexTest() => isProperty<int>();
        [TestMethod] public void TotalPagesTest()
            => isReadOnly((int) Math.Ceiling((double) obj.dbSet.Count() / obj.PageSize));
        [TestMethod] public void HasNextPageTest() => isReadOnly( obj.PageIndex < obj.TotalPages);
        [TestMethod] public void HasPreviousPageTest() => isReadOnly(obj.PageIndex > 1);
        [TestMethod] public void PageSizeTest() {
            Assert.AreEqual(obj.PageSize, Constants.DefaultPageSize);
            isProperty(() => obj.PageSize, x => obj.PageSize = x);
        }

        [TestMethod] public void GetListTest() {
            for (var i = 0; i < GetRandom.UInt8(10, 30); i++) {
                var d = GetRandom.ObjectOf<MeasureData>();
                obj.Add(MeasureFactory.Create(d));
            }

            obj.PageIndex = 1;
            var l = obj.Get();
            Assert.AreEqual(obj.PageSize, l.Count);
            obj.PageIndex = -1;
            l = obj.Get();
            Assert.AreEqual(obj.dbSet.Count(), l.Count);
        }
    }
}
