using Abc.Data.Quantities;
using Abc.Facade.Common;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {
    [TestClass] public class UnitViewTests : SealedTests<UnitView, MetricBaseView> {
        [TestMethod] public void MeasureIdTest() => isNullableProperty<string>("Measure", true);
        [TestMethod] public void UnitTypeTest() => isProperty<UnitType>("Type", true);
        [TestMethod] public void FormulaTest() => isNullableProperty<string>("Formula");
        [TestMethod] public void CodeTest() => isNullableProperty<string>("Code", true);
    }
}
