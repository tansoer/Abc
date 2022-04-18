using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Infra.Common;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Roles {

    public abstract class RoleRepoTests<TRepo, TDomain, TData> :SealedTests<TRepo,
        PagedRepo<TDomain, TData>>
        where TRepo : PagedRepo<TDomain, TData>
        where TData : EntityBaseData, new()
        where TDomain : IEntity<TData> {
        protected RoleDb db;
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<RoleDb>().UseInMemoryDatabase("TestDb").Options;
            db = new RoleDb(options);
        }
        [TestMethod] public void CanSetContextAndSetTest() {
            obj = getObject(db);
            Assert.AreSame(db, obj.db);
            Assert.AreSame(getSet(db), obj.dbSet);
        }
        protected abstract TRepo getObject(RoleDb db);
        protected abstract DbSet<TData> getSet(RoleDb db);
        [TestMethod] public void ToDomainObjectTest() {
            var d = (TData)GetRandom.ObjectOf(typeof(TData));
            var o = getDomainObject(d);
            allPropertiesAreEqual(d, o.Data);
        }
        protected virtual TDomain getDomainObject(TData d) => obj.toDomainObject(d);
    }
}

