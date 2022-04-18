using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Infra.Common;
using Abc.Infra.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Currencies {

    public abstract class
        MoneyRepoTests<TRepo, TDomain, TData> : SealedTests<TRepo, PagedRepo<TDomain, TData>>
        where TRepo : PagedRepo<TDomain, TData>
        where TData : BaseData, new()
        where TDomain : IBaseEntity<TData> {
        protected MoneyDb db;
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<MoneyDb>().UseInMemoryDatabase("TestDb").Options;
            db = new MoneyDb(options);
        }
        [TestMethod] public void CanSetContextAndSetTest() {
            obj = getObject(db);
            Assert.AreSame(db, obj.db);
            Assert.AreSame(getSet(db), obj.dbSet);
        }
        protected abstract TRepo getObject(MoneyDb db);
        protected abstract DbSet<TData> getSet(MoneyDb db);
        [TestMethod] public void ToDomainObjectTest() {
            var d = (TData) GetRandom.ObjectOf(typeof(TData));
            var o = obj.toDomainObject(d);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}

