using Abc.Aids.Enums;
using Abc.Aids.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Enums {

    [TestClass]
    public class NameTypeTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(NameType);

        [TestMethod] public void CountTest() => Assert.AreEqual(6, GetEnum.Count<NameType>());

        [TestMethod]
        public void UndefinedTest() =>
            Assert.AreEqual(0, (int) NameType.Undefined);

        [TestMethod]
        public void OfficialTest() =>
            Assert.AreEqual(1, (int) NameType.Official);

        [TestMethod]
        public void MaidenTest() =>
            Assert.AreEqual(2, (int) NameType.Maiden);

        [TestMethod]
        public void NickTest() =>
            Assert.AreEqual(3, (int) NameType.Nick);

        [TestMethod]
        public void TradeTest() =>
            Assert.AreEqual(4, (int) NameType.Trade);

        [TestMethod]
        public void OtherTest() =>
            Assert.AreEqual(9, (int) NameType.Other);


    }

}
