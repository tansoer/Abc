using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class SubstanceTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Substance);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(Substance.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(2, Substance.Units.Count);
    }

}