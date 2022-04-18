using Abc.Facade.Common;
using Abc.Facade.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Currencies {
    [TestClass] public class CurrencyViewTests : SealedTests<CurrencyView, EntityBaseView> {
        [TestMethod] public void NumericCodeTest() => isNullableProperty<string>("Numeric code");
        [TestMethod] public void MajorUnitSymbolTest() => isNullableProperty<string>("Major unit symbol");
        [TestMethod] public void MinorUnitSymbolTest() => isNullableProperty<string>("Minor unit symbol");
        [TestMethod] public void RatioOfMinorUnitTest() => isProperty<double>("Ratio of minor unit");
        [TestMethod] public void IsIsoCurrencyTest() => isProperty<bool>("Is ISO currency");
        [TestMethod] public void FullNameTest() => isNullableProperty<string>("Full name");
        [TestMethod] public void FormulaTest() => isNullableProperty<string>("Formula");
        [TestMethod] public void CodeTest() => isNullableProperty<string>("Code", true);
    }
}
