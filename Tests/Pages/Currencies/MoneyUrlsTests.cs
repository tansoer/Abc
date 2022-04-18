using Abc.Pages.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Currencies {

    [TestClass]
    public class MoneyUrlsTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(MoneyUrls);
        [TestMethod] public void CountriesTest() => Assert.AreEqual("/Currencies/Countries", MoneyUrls.Countries);
        [TestMethod] public void CardsTest() => Assert.AreEqual("/Currencies/Cards", MoneyUrls.Cards);
        [TestMethod] public void CurrenciesTest() => Assert.AreEqual("/Currencies/Currencies", MoneyUrls.Currencies);
        [TestMethod] public void CurrencyUsagesTest() => Assert.AreEqual("/Currencies/CurrencyUsages", MoneyUrls.CurrencyUsages);
        [TestMethod] public void RateTypesTest() => Assert.AreEqual("/Currencies/RateTypes", MoneyUrls.RateTypes);
        [TestMethod] public void ExchangeRulesTest() => Assert.AreEqual("/Currencies/ExchangeRules", MoneyUrls.ExchangeRules);
        [TestMethod] public void ExchangeRatesTest() => Assert.AreEqual("/Currencies/ExchangeRates", MoneyUrls.ExchangeRates);
        [TestMethod] public void PaymentMethodsTest() => Assert.AreEqual("/Currencies/PaymentMethods", MoneyUrls.PaymentMethods);
        [TestMethod] public void PaymentsTest() => Assert.AreEqual("/Currencies/Payments", MoneyUrls.Payments);
    }

}