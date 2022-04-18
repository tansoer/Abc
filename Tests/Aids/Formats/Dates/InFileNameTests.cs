using Abc.Aids.Formats.Dates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Formats.Dates {

    [TestClass]
    public class InFileNameTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(InFileName);

        [TestMethod]
        public void LongTest()
            => Assert.AreEqual("yyyy.MM.dd.HH.mm.ss", InFileName.Long);

        [TestMethod]
        public void ShortTest()
            => Assert.AreEqual("yyyy.MM.dd", InFileName.Short);

        [TestMethod]
        public void ArchiveTest()
            => Assert.AreEqual("dd.MM.yyyy.HH.mm.ss", InFileName.Archive);

    }

}
