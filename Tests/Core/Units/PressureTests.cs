
using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {
    [TestClass] public class PressureTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(Pressure);
        [TestMethod] public void MeasureTest() => Assert.IsInstanceOfType(Pressure.Measure, typeof(UnitInfo));
        [TestMethod] public void UnitsTest() => Assert.AreEqual(2, Pressure.Units.Count);
    }
}
