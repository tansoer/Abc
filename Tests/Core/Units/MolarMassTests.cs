using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {
    [TestClass]
    public class MolarMassTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(MolarMass);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(MolarMass.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(1, MolarMass.Units.Count);
    }
}