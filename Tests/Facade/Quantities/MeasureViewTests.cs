using Abc.Data.Quantities;
using Abc.Facade.Common;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {

    [TestClass] public class MeasureViewTests : SealedTests<MeasureView, MetricBaseView> {
        [TestMethod] public void MeasureTypeTest() => isProperty<MeasureType>();
        [TestMethod] public void FormulaTest() => isProperty<string>();
        [TestMethod] public void CodeTest() => isProperty<string>();
    }
}
