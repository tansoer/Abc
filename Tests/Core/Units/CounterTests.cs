using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class CounterTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Counter);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(Counter.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(7, Counter.Units.Count);
    }
}