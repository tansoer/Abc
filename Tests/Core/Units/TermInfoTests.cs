using Abc.Aids.Random;
using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class TermInfoTests : TestsBase {

        private string id;
        private sbyte power;
        private TermInfo t;

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(TermInfo);
            id = rndStr;
            power = GetRandom.Int8();
            t = new TermInfo(id, power);
        }

        [TestMethod] public void TermIdTest() => Assert.AreEqual(id, t.TermId);

        [TestMethod] public void PowerTest() => Assert.AreEqual(power, t.Power);

    }

}