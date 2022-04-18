using Abc.Pages.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Currencies {
    [TestClass]
    public class MoneyTitlesTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(MoneyTitles);
        [TestMethod] public void CountriesTest() => Assert.AreEqual("Countries", MoneyTitles.Countries);
        [TestMethod] public void CardsTest() => Assert.AreEqual("Payment cards", MoneyTitles.Cards);
        [TestMethod] public void CurrenciesTest() => Assert.AreEqual("Currencies", MoneyTitles.Currencies);
        [TestMethod] public void CurrencyUsagesTest() => Assert.AreEqual("Currency usages", MoneyTitles.CurrencyUsages);
        [TestMethod] public void RateTypesTest() => Assert.AreEqual("Rate types", MoneyTitles.RateTypes);
        [TestMethod] public void ExchangeRulesTest() => Assert.AreEqual("Exchange rules", MoneyTitles.ExchangeRules);
        [TestMethod] public void ExchangeRatesTest() => Assert.AreEqual("Exchange rates", MoneyTitles.ExchangeRates);
        [TestMethod] public void PaymentMethodsTest() => Assert.AreEqual("Payment methods", MoneyTitles.PaymentMethods);
        [TestMethod] public void PaymentsTest() => Assert.AreEqual("Payments", MoneyTitles.Payments);
    }

}