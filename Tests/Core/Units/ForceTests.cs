
using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {
    [TestClass] public class ForceTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(Force);
        [TestMethod] public void MeasureTest() => Assert.IsInstanceOfType(Force.Measure, typeof(UnitInfo));
        [TestMethod] public void UnitsTest() => Assert.AreEqual(1, Force.Units.Count);
    }
}
