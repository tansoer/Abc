using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class LuminosTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Luminos);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(Luminos.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(3, Luminos.Units.Count);
    }

}