using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class DistanceTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Distance);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(Area.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(33, Distance.Units.Count);
    }

}