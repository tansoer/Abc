using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    public abstract class
        ProductRepoTests<TRepo, TDomain, TData> : SealedTests<TRepo,
            PagedRepo<TDomain, TData>>
        where TRepo : PagedRepo<TDomain, TData>
        where TData : EntityBaseData, new()
        where TDomain : IEntity<TData> {

        protected ProductDb db;

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<ProductDb>().UseInMemoryDatabase("TestDb").Options;
            db = new ProductDb(options);
        }

        [TestMethod]
        public void CanSetContextAndSetTest() {
            obj = getObject(db);
            Assert.AreSame(db, obj.db);
            Assert.AreSame(getSet(db), obj.dbSet);
        }

        protected abstract TRepo getObject(ProductDb db);

        protected abstract DbSet<TData> getSet(ProductDb db);

        [TestMethod]
        public virtual void ToDomainObjectTest() {
            var d = (TData) GetRandom.ObjectOf(typeof(TData));
            var o = obj.toDomainObject(d);
            allPropertiesAreEqual(d, o.Data);
        }

    }

}

