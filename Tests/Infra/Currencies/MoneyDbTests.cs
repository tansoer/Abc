using Abc.Data.Currencies;
using Abc.Infra;
using Abc.Infra.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Currencies {
    [TestClass] public class MoneyDbTests : DbTests<MoneyDb, BaseDb<MoneyDb>> {
        private class testClass : MoneyDb {
            public testClass(DbContextOptions<MoneyDb> o) : base(o) { }
            public ModelBuilder RunOnModelCreating() {
                var set = new ConventionSet();
                var mb = new ModelBuilder(set);
                OnModelCreating(mb);
                return mb;
            }
        }
        protected override MoneyDb createObject() {
            options = new DbContextOptionsBuilder<MoneyDb>().UseInMemoryDatabase("TestDb").Options;
            return new MoneyDb(options);
        }
        [TestMethod] public void InitializeTablesTest() {
            MoneyDb.InitializeTables(null);
            var o = new testClass(options);
            var builder = o.RunOnModelCreating();
            MoneyDb.InitializeTables(builder);
            testEntity<CurrencyUsageData>(builder);
            testEntity<PaymentMethodData>(builder);
            testEntity<PaymentMethodData>(builder);
            testEntity<ExchangeRateData>(builder);
            testEntity<RateTypeData>(builder);
            testEntity<CurrencyData>(builder);
            testEntity<CardAssociationData>(builder);
            testEntity<ExchangeRuleData>(builder);
        }
        [TestMethod] public void CurrencyUsagesTest() => isNullable<DbSet<CurrencyUsageData>>();
        [TestMethod] public void CardAssociationsTest() => isNullable<DbSet<CardAssociationData>>();
        [TestMethod] public void CurrenciesTest() => isNullable<DbSet<CurrencyData>>();
        [TestMethod] public void CountriesTest() => isNullable<DbSet<CountryData>>();
        [TestMethod] public void RatesTest() => isNullable<DbSet<ExchangeRateData>>();
        [TestMethod] public void PaymentMethodsTest() => isNullable<DbSet<PaymentMethodData>>();
        [TestMethod] public void RateRulesTest() => isNullable<DbSet<ExchangeRuleData>>();
        [TestMethod] public void RateTypesTest() => isNullable<DbSet<RateTypeData>>();
    }
}
