using Abc.Data.Common;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {
    [TestClass]
    public class CurrencyDataTests : SealedTests<CurrencyData, MetricBaseData> {

        [TestMethod] public void NumericCodeTest() => isNullable<string>();

        [TestMethod] public void MajorUnitSymbolTest() => isNullable<string>();

        [TestMethod] public void MinorUnitSymbolTest() => isNullable<string>();

        [TestMethod] public void RatioOfMinorUnitTest() => isProperty<double>();

        [TestMethod] public void IsIsoCurrencyTest() => isProperty<bool>();
    }
}
