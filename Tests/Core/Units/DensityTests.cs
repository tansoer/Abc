using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {
    [TestClass]
    public class DensityTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Density);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(Density.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(2, Density.Units.Count);
    }
}