using Abc.Aids.Methods;
using Abc.Aids.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Methods {

    [TestClass]
    public class ComposeTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Compose);

        [TestMethod]
        public void IdTest() {
            var h = rndStr;
            var t = rndStr;
            var actual = Compose.Id(h, t);
            var expected = $"{h}.{t}";
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(".", Compose.Id(null, null));
        }

    }

}
