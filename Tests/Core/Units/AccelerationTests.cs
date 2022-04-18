
using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {
    [TestClass] public class AccelerationTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(Acceleration);
        [TestMethod] public void MeasureTest() => Assert.IsInstanceOfType(Acceleration.Measure, typeof(UnitInfo));
        [TestMethod] public void UnitsTest() => Assert.AreEqual(1, Acceleration.Units.Count);
    }
}
