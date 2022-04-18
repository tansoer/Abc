using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Contacts;
using Abc.Infra.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Contacts {

    [TestClass]
    public class DeviceRegistrationTests :SealedTests<DeviceRegistration, Entity<DeviceRegistrationData>> {

        private PartyContactsRepo contacts;
        private PartyDb db;

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            contacts = GetRepo.Instance<IPartyContactsRepo>() as PartyContactsRepo;
            db = contacts?.db as PartyDb;
        }

        [TestCleanup]
        public override void TestCleanup() {
            removeAll(contacts?.dbSet, db);
            contacts = null;
            db = null;
            base.TestCleanup();
        }

        protected override DeviceRegistration createObject()
            => new DeviceRegistration(random<DeviceRegistrationData>());
        [TestMethod] public void addressIdTest() => isReadOnly(obj.Data.PostalAddressId);
        [TestMethod] public void deviceIdTest() => isReadOnly(obj.Data.TelecomDeviceId);
    }
}
