using Abc.Pages.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Quantities {

    [TestClass] public class QuantityUrlsTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(QuantityUrls);
        [TestMethod] public void MeasuresTest() => Assert.AreEqual("/Quantities/Measures", QuantityUrls.Measures);
        [TestMethod] public void MeasureTermsTest() =>
            areEqual("/Quantities/MeasureTerms", QuantityUrls.MeasureTerms);
        [TestMethod] public void SystemsOfUnitsTest() =>
            areEqual("/Quantities/SystemsOfUnits", QuantityUrls.SystemsOfUnits);
        [TestMethod] public void UnitFactorsTest() =>
            areEqual("/Quantities/UnitFactors", QuantityUrls.UnitFactors);
        [TestMethod] public void UnitsTest() =>
            areEqual("/Quantities/Units", QuantityUrls.Units);
        [TestMethod] public void UnitTermsTest() =>
            areEqual("/Quantities/UnitTerms", QuantityUrls.UnitTerms);
    }
}
