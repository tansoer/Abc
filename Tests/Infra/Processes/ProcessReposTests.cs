using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Processes {

    public abstract class
        ProcessReposTests<TRepo, TDomain, TData> :SealedTests<TRepo, PagedRepo<TDomain, TData>>
        where TRepo : PagedRepo<TDomain, TData>
        where TData : EntityBaseData, new()
        where TDomain : IEntity<TData> {
        protected ProcessDb db;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<ProcessDb>().UseInMemoryDatabase("TestDb").Options;
            db = new ProcessDb(options);
        }
        [TestMethod]
        public void CanSetContextAndSetTest() {
            obj = getObject(db);
            Assert.AreSame(db, obj.db);
            Assert.AreSame(getSet(db), obj.dbSet);
        }
        protected abstract TRepo getObject(ProcessDb db);
        protected abstract DbSet<TData> getSet(ProcessDb db);
        [TestMethod]
        public void ToDomainObjectTest() {
            var d = (TData)GetRandom.ObjectOf(typeof(TData));
            var o = obj.toDomainObject(d);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}