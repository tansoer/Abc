using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Infra.Common;
using Abc.Infra.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Inventory {

    public abstract class
        InventoryRepoTests<TRepo, TDomain, TData> :SealedTests<TRepo, PagedRepo<TDomain, TData>>
        where TRepo : PagedRepo<TDomain, TData>
        where TData : EntityBaseData, new()
        where TDomain : Entity<TData> {
        protected InventoryDb db;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<InventoryDb>().UseInMemoryDatabase("TestDb").Options;
            db = new InventoryDb(options);
        }
        [TestMethod]
        public void CanSetContextAndSetTest() {
            obj = getObject(db);
            Assert.AreSame(db, obj.db);
            Assert.AreSame(getSet(db), obj.dbSet);
        }
        protected abstract TRepo getObject(InventoryDb db);
        protected abstract DbSet<TData> getSet(InventoryDb db);
        [TestMethod]
        public void ToDomainObjectTest() {
            var d = (TData)GetRandom.ObjectOf(typeof(TData));
            var o = obj.toDomainObject(d);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}