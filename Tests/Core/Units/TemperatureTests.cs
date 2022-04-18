using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class TemperatureTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Temperature);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(Temperature.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(4, Temperature.Units.Count);
    }

}