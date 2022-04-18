using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {
    [TestClass]
    public class AcidityTests : TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(Acidity);

        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(Acidity.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(1, Acidity.Units.Count);
    }
}