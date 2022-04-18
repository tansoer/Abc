using System.Linq;
using Abc.Infra.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Currencies {
    [TestClass] public class MoneyDbInitializerTests :DbInitializerTests<MoneyDb> {
        public MoneyDbInitializerTests() {
            type = typeof(MoneyDbInitializer);
            db = new MoneyDb(options);
            MoneyDbInitializer.Initialize(db, true);
        }
        [TestMethod] public void InitializeTest() {    }
        [TestMethod] public void RatesTest() => areEqual(31, db.Rates.Count());
        [TestMethod] public void RateRulesTest() => areEqual(0, db.RateRules.Count());
        [TestMethod] public void PaymentMethodsTest() => areEqual(0, db.PaymentMethods.Count());
        [TestMethod] public void CardAssociationsTest() => areEqual(4, db.CardAssociations.Count());
        [TestMethod] public void RateTypesTest() => areEqual(4, db.RateTypes.Count());
        [TestMethod] public void CurrenciesTest() => areEqual(106, db.Currencies.Count());
        [TestMethod] public void CurrencyUsagesTest() => areEqual(135, db.CurrencyUsages.Count());
        [TestMethod] public void CountriesTest() => areEqual(0, db.Countries.Count());
    }
}
