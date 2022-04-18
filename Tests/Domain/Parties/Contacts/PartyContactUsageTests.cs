using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Contacts;
using Abc.Infra.Classifiers;
using Abc.Infra.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Contacts {
    [TestClass]
    public class PartyContactUsageTests : SealedTests<PartyContactUsage, BasePartyAttribute<PartyContactUsageData>> {
        private PartyContactsRepo contacts;
        private PartyDb db;
        private ClassifiersRepo contactUsageTypes;
        private ClassifierDb classifiersDb;
        protected override PartyContactUsage createObject() => new (random<PartyContactUsageData>());
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            contacts = GetRepo.Instance<IPartyContactsRepo>() as PartyContactsRepo;
            contactUsageTypes = GetRepo.Instance<IClassifiersRepo>() as ClassifiersRepo;
            db = contacts?.db as PartyDb;
            classifiersDb = contactUsageTypes?.db as ClassifierDb;
        }
        [TestCleanup] public override void TestCleanup() {
            removeAll(db?.PartyContacts, db);
            removeAll(classifiersDb?.Classifiers, classifiersDb);
            base.TestCleanup();
        }
        [TestMethod] public void contactIdTest() => isReadOnly(obj.Data.Name);
        [TestMethod] public void typeIdTest() => isReadOnly(obj.Data.Code);
        [TestMethod] public void ContactTest() {
            var d = GetRandom.ObjectOf<PartyContactData>();
            if (d.ContactType == ContactType.Unspecified) d.ContactType = ContactType.Email;
            d.Id = obj.contactId;
            contacts.Add(PartyContactFactory.Create(d));
            var t = isReadOnly() as IPartyContact;
            Assert.IsNotNull(t);
            allPropertiesAreEqual(d, t.Data);
        }
        [TestMethod] public void TypeTest() {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = ClassifierType.ContactUsage;
            d.Id = obj.typeId;
            contactUsageTypes.Add(new ContactUsageType(d));
            var t = isReadOnly() as ContactUsageType;
            Assert.IsNotNull(t);
            allPropertiesAreEqual(d, t.Data);
        }
    }
}
