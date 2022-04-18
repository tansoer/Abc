using System;
using System.Linq;
using System.Linq.Expressions;
using Abc.Aids.Reflection;
using Abc.Data.Classifiers;
using Abc.Infra;
using Abc.Infra.Classifiers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Classifiers {
    [TestClass]
    public class ClassifierDbTests :BaseClassTests<ClassifierDb, BaseDb<ClassifierDb>> {
        private DbContextOptions<ClassifierDb> options;

        private class testClass :ClassifierDb {
            public testClass(DbContextOptions<ClassifierDb> o) :base(o) { }

            public ModelBuilder RunOnModelCreating() {
                var set = new ConventionSet();
                var mb = new ModelBuilder(set);
                OnModelCreating(mb);

                return mb;
            }
        }

        protected override ClassifierDb createObject() {
            options = new DbContextOptionsBuilder<ClassifierDb>().UseInMemoryDatabase("TestDb").Options;
            return new ClassifierDb(options);
        }

        [TestMethod]
        public void InitializeTablesTest() {
            ClassifierDb.InitializeTables(null);
            var o = new testClass(options);
            var builder = o.RunOnModelCreating();
            ClassifierDb.InitializeTables(builder);
            testEntity<ClassifierData>(builder);
        }

        [TestMethod]
        public void ClassifiersTest() => isNullable<DbSet<ClassifierData>>();

        protected static void testKey<T>(IMutableEntityType entity, params Expression<Func<T, object>>[] values) {
            var key = entity.FindPrimaryKey();
            if (values is null) Assert.IsNull(key);
            else
                foreach (var v in values) {
                    var name = GetMember.Name(v);
                    Assert.IsNotNull(key.Properties.FirstOrDefault(x => x.Name == name));
                }
        }

        protected static void testEntity<T>(ModelBuilder b, params Expression<Func<T, object>>[] values) {
            var name = typeof(T).FullName ?? string.Empty;
            var entity = b.Model.FindEntityType(name);
            Assert.IsNotNull(entity, name);
            testKey(entity, values);
        }
    }
}