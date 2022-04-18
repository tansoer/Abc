using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Infra.Classifiers;
using Abc.Infra.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Classifiers {

    [TestClass]
    public class ClassifiersRepoTests
        :SealedTests<ClassifiersRepo, PagedRepo<IClassifier, ClassifierData>> {
        private ClassifierDb db;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<ClassifierDb>().UseInMemoryDatabase("TestDb").Options;
            db = new ClassifierDb(options);
        }
        [TestMethod]
        public void CanSetContextAndSetTest() {
            obj = getObject(db);
            Assert.AreSame(db, obj.db);
            Assert.AreSame(getSet(db), obj.dbSet);
        }
        protected static ClassifiersRepo getObject(ClassifierDb db) => new ClassifiersRepo(db);
        protected static DbSet<ClassifierData> getSet(ClassifierDb c) => c.Classifiers;
        [TestMethod] public void ToDomainObjectTest() {
            var d = random<ClassifierData>();
            d.ClassifierType = ClassifierType.Order;
            var o = getDomainObject(d);
            allPropertiesAreEqual(d, o.Data);
        }
        protected IClassifier getDomainObject(ClassifierData d) => obj.toDomainObject(d);
    }
}
