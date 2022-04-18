using Abc.Data.Quantities;
using Abc.Infra;
using Abc.Infra.Quantities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Quantities {

    [TestClass] public class QuantityDbTests: 
        DbTests<QuantityDb, BaseDb<QuantityDb>> {
        private class testClass : QuantityDb {
            public testClass(DbContextOptions<QuantityDb> o) : base(o) { }
            public ModelBuilder RunOnModelCreating() {
                var set = new ConventionSet();
                var mb = new ModelBuilder(set);
                OnModelCreating(mb);
                return mb;
            }
        }
        protected override QuantityDb createObject() {
            options = new DbContextOptionsBuilder<QuantityDb>().UseInMemoryDatabase("TestDb").Options;
            return new QuantityDb(options);
        }
        [TestMethod] public void InitializeTablesTest() {
            QuantityDb.InitializeTables(null);
            var o = new testClass(options);
            var builder = o.RunOnModelCreating();
            QuantityDb.InitializeTables(builder);
            testEntity<SystemOfUnitsData>(builder);
            testEntity<CommonTermData>(builder);
            testEntity<MeasureData>(builder);
            testEntity<UnitData>(builder);
            testEntity<UnitFactorData>(builder);
            testEntity<UnitRulesData>(builder);
        }
        [TestMethod] public void MeasuresTest() => isNullable<DbSet<MeasureData>>();
        [TestMethod] public void UnitsTest() => isNullable<DbSet<UnitData>>();
        [TestMethod] public void SystemsOfUnitsTest() => isNullable<DbSet<SystemOfUnitsData>>();
        [TestMethod] public void UnitFactorsTest() => isNullable<DbSet<UnitFactorData>>();
        [TestMethod] public void UnitRulesTest() => isNullable<DbSet<UnitRulesData>>();
        [TestMethod] public void CommonTermsTest() => isNullable<DbSet<CommonTermData>>();
    }
}
