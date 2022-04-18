using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class CurrentTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Current);
        [TestMethod]
        public void MeasureTest()
            => Assert.IsInstanceOfType(Current.Measure, typeof(UnitInfo));

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(0, Current.Units.Count);
    }

}