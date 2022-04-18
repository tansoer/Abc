using Abc.Data.Rules;
using Abc.Infra;
using Abc.Infra.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Rules {

    [TestClass]
    public class RuleDbTests : DbTests<RuleDb, BaseDb<RuleDb>> {
        private class testClass : RuleDb {
            public testClass(DbContextOptions<RuleDb> o) : base(o) { }
            public ModelBuilder RunOnModelCreating() {
                var set = new ConventionSet();
                var mb = new ModelBuilder(set);
                OnModelCreating(mb);
                return mb;
            }
        }
        protected override RuleDb createObject() {
            options = new DbContextOptionsBuilder<RuleDb>().UseInMemoryDatabase("TestDb").Options;
            return new RuleDb(options);
        }
        [TestMethod] public void InitializeTablesTest() {
            RuleDb.InitializeTables(null);
            var o = new testClass(options);
            var builder = o.RunOnModelCreating();
            RuleDb.InitializeTables(builder);
            testEntity<RuleElementData>(builder);
            testEntity<RuleData>(builder);
            testEntity<RuleOverrideData>(builder);
            testEntity<RuleSetData>(builder);
            testEntity<RuleContextData>(builder);
        }
        [TestMethod] public void RuleOverridesTest() => isNullable<DbSet<RuleOverrideData>>();
        [TestMethod] public void RulesTest() => isNullable<DbSet<RuleData>>();
        [TestMethod] public void RuleElementsTest() => isNullable<DbSet<RuleElementData>>();
        [TestMethod] public void RuleContextsTest() => isNullable<DbSet<RuleContextData>>();
        [TestMethod] public void RuleSetsTest() => isNullable<DbSet<RuleSetData>>();
        [TestMethod] public void RuleUsagesTest() => isNullable<DbSet<RuleUsageData>>();
    }
}
