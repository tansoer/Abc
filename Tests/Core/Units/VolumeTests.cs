using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class VolumeTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Volume);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(Volume.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(18, Volume.Units.Count);
    }

}