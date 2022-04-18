using Abc.Data.Common;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {

    [TestClass]
    public class CurrencyUsageDataTests : SealedTests<CurrencyUsageData, DetailedData> {

        [TestMethod] public void CountryIdTest() => isNullable<string>();

        [TestMethod] public void CurrencyIdTest() => isNullable<string>();

        [TestMethod] public void CurrencyNativeNameTest() => isNullable<string>();
    }

}