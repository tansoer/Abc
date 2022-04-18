using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties {
    [TestClass]
    public class PartyFactoryTests :TestsBase{
        
        private PartyData d;
        [TestInitialize]
        public void TestInitialize() {
            type = typeof(PartyFactory);
            d = GetRandom.ObjectOf<PartyData>();
        }

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreatePersonTest() {
            d.PartyType = PartyType.Person;
            var o = PartyFactory.Create(d);
            Assert.IsInstanceOfType(o, typeof(Person));
            allPropertiesAreEqual(d, o.Data);
        }
        [TestMethod]
        public void CreateOrganizationTest() {
            d.PartyType = PartyType.Organization;
            var o = PartyFactory.Create(d);
            Assert.IsInstanceOfType(o, typeof(Organization));
            allPropertiesAreEqual(d, o.Data);
        }

        [TestMethod]
        public void CreatePersonDataTest() {
            d.PartyType = PartyType.Person;
            var o = PartyFactory.Create(d);
            allPropertiesAreEqual(d, PartyFactory.Create(o));
        }
        [TestMethod]
        public void CreateOrganizationDataTest() {
            d.PartyType = PartyType.Organization;
            var o = PartyFactory.Create(d);
            allPropertiesAreEqual(d, PartyFactory.Create(o));
        }
    }
}
