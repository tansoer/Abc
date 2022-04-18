
using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {
    [TestClass] public class AreaTests : TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(Area);
        [TestMethod] public void MeasureTest() => Assert.IsInstanceOfType(Area.Measure, typeof(UnitInfo));
        [TestMethod] public void UnitsTest() => Assert.AreEqual(16, Area.Units.Count);
    }
}
