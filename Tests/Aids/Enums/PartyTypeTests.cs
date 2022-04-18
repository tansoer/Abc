using Abc.Aids.Enums;
using Abc.Aids.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Enums {
    public class PartyTypeTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(PartyType);

        [TestMethod] public void CountTest() => Assert.AreEqual(3, GetEnum.Count<PartyType>());

        [TestMethod]
        public void UnspecifiedTest() =>
            Assert.AreEqual(0, (int) PartyType.Unspecified);

        [TestMethod]
        public void OrganizationTest() =>
            Assert.AreEqual(1, (int) PartyType.Organization);

        [TestMethod]
        public void PersonTest() =>
            Assert.AreEqual(2, (int) PartyType.Person);
    }

}
