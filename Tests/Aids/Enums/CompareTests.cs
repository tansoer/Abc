using Abc.Aids.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Enums {

    [TestClass]
    public class CompareTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Compare);

        [TestMethod]
        public void EqualTest()
            => Assert.AreEqual(0, Compare.Equal);

        [TestMethod]
        public void GreaterTest()
            => Assert.AreEqual(1, Compare.Greater);

        [TestMethod]
        public void LessTest()
            => Assert.AreEqual(-1, Compare.Less);

    }

}