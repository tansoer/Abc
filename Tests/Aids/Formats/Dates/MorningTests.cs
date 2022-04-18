using Abc.Aids.Formats.Dates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Formats.Dates {

    [TestClass]
    public class MorningTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Morning);

        [TestMethod]
        public void LongTest()
            => Assert.AreEqual("08:00:00", Morning.Long);

        [TestMethod]
        public void ShortTest()
            => Assert.AreEqual("08:00", Morning.Short);

    }

}
