using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class TimeTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Time);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(Time.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(14, Time.Units.Count);
    }

}