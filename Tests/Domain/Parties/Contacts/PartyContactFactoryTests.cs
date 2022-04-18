using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Contacts {
    [TestClass]
    public class PartyContactFactoryTests : TestsBase {

        private PartyContactData d;
        [TestInitialize]
        public void TestInitialize() {
            type = typeof(PartyContactFactory);
            d = GetRandom.ObjectOf<PartyContactData>();
        }

        [TestMethod] public void CreateTest() { }
        [TestMethod]
        public void CreateEmailAddressTest() {
            d.ContactType = ContactType.Email;
            var o = PartyContactFactory.Create(d);
            isInstanceOfType(o, typeof(EmailAddress));
            allPropertiesAreEqual(d, o.Data);
        }
        [TestMethod]
        public void CreateWebPageAddressTest() {
            d.ContactType = ContactType.Web;
            var o = PartyContactFactory.Create(d);
            isInstanceOfType(o, typeof(WebPageAddress));
            allPropertiesAreEqual(d, o.Data);
        }
        [TestMethod]
        public void CreateGeographicAddressTest() {
            d.ContactType = ContactType.Postal;
            var o = PartyContactFactory.Create(d);
            isInstanceOfType(o, typeof(GeographicAddress));
            allPropertiesAreEqual(d, o.Data);
        }
        [TestMethod]
        public void CreateTelecomAadressTest() {
            d.ContactType = ContactType.Telecom;
            var o = PartyContactFactory.Create(d);
            isInstanceOfType(o, typeof(TelecomAddress));
            allPropertiesAreEqual(d, o.Data);

        }
        [TestMethod]
        public void CreateEmailAddressDataTest() {
            d.ContactType = ContactType.Email;
            var o = PartyContactFactory.Create(d);
            allPropertiesAreEqual(d, PartyContactFactory.Create(o));
        }
        [TestMethod]
        public void CreateWebAddressDataTest() {
            d.ContactType = ContactType.Web;
            var o = PartyContactFactory.Create(d);
            allPropertiesAreEqual(d, PartyContactFactory.Create(o));

        }
        [TestMethod]
        public void CreateTelecomAddressDataTest() {
            d.ContactType = ContactType.Telecom;
            var o = PartyContactFactory.Create(d);
            allPropertiesAreEqual(d, PartyContactFactory.Create(o));

        }
        [TestMethod]
        public void CreateGeographicAddressDataTest() {
            d.ContactType = ContactType.Postal;
            var o = PartyContactFactory.Create(d);
            allPropertiesAreEqual(d, PartyContactFactory.Create(o));

        }
    }
}
