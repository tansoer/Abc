using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class SystemOfUnitsTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(SystemOfUnits);

        [TestMethod]
        public void UnitsTest()
            => Assert.AreEqual(2, SystemOfUnits.Units.Count);

        [TestMethod]
        public void SiSystemIdTest()
            => Assert.AreEqual("SI", SystemOfUnits.SiSystemId);

        [TestMethod]
        public void CgsSystemIdTest()
            => Assert.AreEqual("CGS", SystemOfUnits.CgsSystemId);

    }

}