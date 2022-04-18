using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Infra.Common;
using Abc.Infra.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Orders {

    public abstract class
        OrderReposTests<TRepo, TDomain, TData> :SealedTests<TRepo, PagedRepo<TDomain, TData>>
        where TRepo : PagedRepo<TDomain, TData>
        where TData : EntityBaseData, new()
        where TDomain : IEntity<TData> {
        protected OrderDb db;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<OrderDb>().UseInMemoryDatabase("TestDb").Options;
            db = new OrderDb(options);
        }
        [TestMethod]
        public void CanSetContextAndSetTest() {
            obj = getObject(db);
            Assert.AreSame(db, obj.db);
            Assert.AreSame(getSet(db), obj.dbSet);
        }
        protected abstract TRepo getObject(OrderDb db);
        protected abstract DbSet<TData> getSet(OrderDb db);
        [TestMethod]
        public void ToDomainObjectTest() {
            var d = (TData)GetRandom.ObjectOf(typeof(TData));
            var o = obj.toDomainObject(d);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}