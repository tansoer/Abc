using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Infra.Common;
using Abc.Infra.Quantities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Quantities {

    public abstract class
        QuantityRepoTests<TRepo, TDomain, TData> : SealedTests<TRepo,
            PagedRepo<TDomain, TData>>
        where TRepo : PagedRepo<TDomain, TData>
        where TData : BaseData, new()
        where TDomain : BaseEntity<TData> {

        protected QuantityDb db;

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<QuantityDb>().UseInMemoryDatabase("TestDb").Options;
            db = new QuantityDb(options);
        }
        [TestMethod] public void CanSetContextAndSetTest() {
            obj = getObject(db);
            Assert.AreSame(db, obj.db);
            Assert.AreSame(getSet(db), obj.dbSet);
        }
        protected abstract TRepo getObject(QuantityDb db);

        protected abstract DbSet<TData> getSet(QuantityDb db);

        [TestMethod]
        public void ToDomainObjectTest() {
            var d = (TData) GetRandom.ObjectOf(typeof(TData));
            var o = obj.toDomainObject(d);
            allPropertiesAreEqual(d, o.Data);
        }

    }

}

