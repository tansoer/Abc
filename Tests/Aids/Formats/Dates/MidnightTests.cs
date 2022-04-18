using Abc.Aids.Formats.Dates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Formats.Dates {

    [TestClass]
    public class MidnightTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Midnight);

        [TestMethod]
        public void LongTest()
            => Assert.AreEqual("00:00:00", Midnight.Long);

        [TestMethod]
        public void ShortTest()
            => Assert.AreEqual("00:00", Midnight.Short);

    }

}
