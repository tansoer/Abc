using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class PersentageTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Persentage);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(Persentage.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(1, Persentage.Units.Count);
    }

}