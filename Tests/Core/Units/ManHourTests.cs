using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class ManHourTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(ManHour);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(ManHour.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(11, ManHour.Units.Count);
    }

}