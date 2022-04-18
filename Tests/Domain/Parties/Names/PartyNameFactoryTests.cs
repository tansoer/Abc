using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Names {

    [TestClass]
    public class PartyNameFactoryTests : TestsBase {

        private PartyNameData d;
        [TestInitialize]
        public void TestInitialize() {
            type = typeof(PartyNameFactory);
            d = GetRandom.ObjectOf<PartyNameData>();
        }

        [TestMethod] public void CreateTest() { }
        [TestMethod]
        public void CreateOrganizationNameTest() {
            d.PartyType = PartyType.Organization;
            var o = PartyNameFactory.Create(d);
            Assert.IsInstanceOfType(o, typeof(OrganizationName));
            allPropertiesAreEqual(d, o.Data);
        }
        [TestMethod]
        public void CreatePersonNameTest() {
            d.PartyType = PartyType.Person;
            var o = PartyNameFactory.Create(d);
            Assert.IsInstanceOfType(o, typeof(PartyName));
            allPropertiesAreEqual(d, o.Data);
        }
        [TestMethod]
        public void CreateOrganizationNameDataTest() {
            d.PartyType = PartyType.Organization;
            var o = PartyNameFactory.Create(d);
            allPropertiesAreEqual(d, PartyNameFactory.Create(o));
        }
        [TestMethod]
        public void CreatePersonNameDataTest() {
            d.PartyType = PartyType.Person;
            var o = PartyNameFactory.Create(d);
            allPropertiesAreEqual(d, PartyNameFactory.Create(o));

        }

    }

}
