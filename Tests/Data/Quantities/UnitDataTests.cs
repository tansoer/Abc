using Abc.Data.Common;
using Abc.Data.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Quantities {
    [TestClass]
    public class UnitDataTests : SealedTests<UnitData, MetricBaseData> {
        
        [TestMethod] public void MeasureIdTest() => isNullable<string>();
        [TestMethod] public void UnitTypeTest() => isProperty<UnitType>();

    }
}
