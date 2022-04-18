using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {
    [TestClass]
    public class MolarityTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Molarity);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(Molarity.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(2, Molarity.Units.Count);
    }
}