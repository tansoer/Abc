using Abc.Data.Common;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {

    [TestClass]
    public class ExchangeRateDataTests : SealedTests<ExchangeRateData, DetailedData> {
        [TestMethod] public void RateTest() => isProperty<decimal>();
        [TestMethod] public void CurrencyIdTest() => isNullable<string>();
        [TestMethod] public void RateTypeIdTest() => isNullable<string>();
    }
}