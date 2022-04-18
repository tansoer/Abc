using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    public abstract class PartyRepoTests<TRepo, TDomain, TData> : SealedTests<TRepo,
        PagedRepo<TDomain, TData>>
        where TRepo : PagedRepo<TDomain, TData>
        where TData : EntityBaseData, new()
        where TDomain : IEntity<TData> {

        protected PartyDb db;

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<PartyDb>().UseInMemoryDatabase("TestDb").Options;
            db = new PartyDb(options);
        }

        [TestMethod]
        public void CanSetContextAndSetTest() {
            obj = getObject(db);
            Assert.AreSame(db, obj.db);
            Assert.AreSame(getSet(db), obj.dbSet);
        }

        protected abstract TRepo getObject(PartyDb db);

        protected abstract DbSet<TData> getSet(PartyDb db);

        [TestMethod]
        public void ToDomainObjectTest() {
            var d = (TData) GetRandom.ObjectOf(typeof(TData));
            var o = getDomainObject(d);
            allPropertiesAreEqual(d, o.Data);
        }
        protected virtual TDomain getDomainObject(TData d) => obj.toDomainObject(d);

    }

}

