using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class FactorsTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Factors);

        [TestMethod]
        public void MicromicroTest()
            => Assert.AreEqual(Factors.Micro * Factors.Micro, Factors.Micromicro);

        [TestMethod]
        public void PicoTest()
            => Assert.AreEqual(0.000000000001, Factors.Pico);

        [TestMethod] public void AngstromTest() => Assert.AreEqual(Factors.Deci * Factors.Nano, Factors.Angstrom);

        [TestMethod]
        public void MillimicroTest()
            => Assert.AreEqual(Factors.Milli * Factors.Micro, Factors.Millimicro);

        [TestMethod]
        public void NanoTest()
            => Assert.AreEqual(0.000000001, Factors.Nano);

        [TestMethod]
        public void MicroTest()
            => Assert.AreEqual(0.000001, Factors.Micro);

        [TestMethod]
        public void MilliTest()
            => Assert.AreEqual(0.001, Factors.Milli);

        [TestMethod]
        public void CentiTest()
            => Assert.AreEqual(0.01, Factors.Centi);

        [TestMethod]
        public void DeciTest()
            => Assert.AreEqual(0.1, Factors.Deci);

        [TestMethod]
        public void BaseTest()
            => Assert.AreEqual(1, Factors.Base);

        [TestMethod]
        public void DecaTest()
            => Assert.AreEqual(10, Factors.Deca);

        [TestMethod]
        public void HectoTest()
            => Assert.AreEqual(100, Factors.Hecto);

        [TestMethod]
        public void KiloTest()
            => Assert.AreEqual(1000, Factors.Kilo);

        [TestMethod]
        public void MegaTest()
            => Assert.AreEqual(1000000, Factors.Mega);

        [TestMethod]
        public void GigaTest()
            => Assert.AreEqual(1000000000, Factors.Giga);

        [TestMethod]
        public void TeraTest()
            => Assert.AreEqual(1000000000000, Factors.Tera);

    }

}