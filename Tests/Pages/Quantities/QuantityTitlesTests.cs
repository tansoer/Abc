using Abc.Pages.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Quantities {
    [TestClass] public class QuantityTitlesTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(QuantityTitles);

        [TestMethod] public void MeasuresTest() => Assert.AreEqual("Measures", QuantityTitles.Measures);
        [TestMethod] public void MeasureTermsTest() => areEqual("Measure terms", QuantityTitles.MeasureTerms);
        [TestMethod] public void SystemsOfUnitsTest() => areEqual("Systems of units", QuantityTitles.SystemsOfUnits);
        [TestMethod] public void UnitFactorsTest() => areEqual("Unit factors", QuantityTitles.UnitFactors);
        [TestMethod]public void UnitsTest() => areEqual("Units", QuantityTitles.Units);
        [TestMethod] public void UnitTermsTest() => areEqual("Unit terms", QuantityTitles.UnitTerms);
    }
}
