using Abc.Data.Common;
using Abc.Data.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Quantities {
    [TestClass]
    public class MeasureDataTests : SealedTests<MeasureData, MetricBaseData> {

        [TestMethod] public void MeasureTypeTest() => isProperty<MeasureType>();
    }

}
