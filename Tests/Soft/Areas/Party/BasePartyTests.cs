using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Common;
using Abc.Data.Parties;
using Abc.Data.Quantities;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Parties.Names;
using Abc.Facade.Common;
using Abc.Infra.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Party {

    public abstract class BasePartyTests<TView, TData> :BaseAcceptanceTests<TView, TData, PartyDb> 
        where TData : EntityBaseData where TView : EntityBaseView {
        protected override void doOnCreated(PartyDb c) => clearAll(c);
        [TestCleanup] public override void TestCleanup() {
            base.TestCleanup();
            clearAll(db);
        }
        protected void clearAll(PartyDb c) {
            if (c is null) return;
            clear(c.BodyMetrics);
            clear(c.BodyMetricTypes);
            clear(c.PartySummaries);
            clear(c.PersonEthnicities);
            clear(c.PartyCapabilities);
            clear(c.Countries);
            clear(c.PartyContactUsages);
            clear(c.PartyContacts);
            clear(c.TelecomDeviceRegistrations);
            clear(c.Identifiers);
            clear(c.RegistrationAuthorities);
            clear(c.PartyNames);
            clear(c.PersonNamePrefixes);
            clear(c.PersonNameSuffixes);
            clear(c.PartyNameUses);
            clear(c.PreferenceOptions);
            clear(c.Preferences);
            clear(c.PartyAuthentications);
            clear(c.PartySignatures);
            clear(c.SignedEntityTypes);
            clear(c.Parties);
            clear(classifiersDb.Classifiers, classifiersDb);
        }
        protected void addPartyName(string id) {
            var d = random<PartyNameData>();
            d.Id = id;
            add<IPartyNamesRepo, PartyName>(PartyNameFactory.Create(d));
        }
        protected void addPartyNameForPartyId(string partyId) {
            var d = random<PartyNameData>();
            d.PartyId = partyId;
            add<IPartyNamesRepo, PartyName>(PartyNameFactory.Create(d));
        }
        protected void addContact(string id) {
            var d = random<PartyContactData>();
            d.Id = id;
            add<IPartyContactsRepo, IPartyContact>(PartyContactFactory.Create(d));
        }
        protected void addParty(string id, PartyType t) {
            var d = GetRandom.ObjectOf<PartyData>();
            d.Id = id;
            d.PartyType = t;
            addToDatabase(d);
        }
        protected void addEthnicity(string id) {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.Id = id;
            d.ClassifierType = ClassifierType.PartyEthnicity;
            classifiersDb.Add(d);
            classifiersDb.SaveChanges();
        }
        protected void addRegistrationAuthority(string id) {
            var d = random<ClassifierData>();
            d.Id = id;
            d.ClassifierType = ClassifierType.RegistrationAuthority;
            classifiersDb.Classifiers.Add(d);
            classifiersDb.SaveChanges();
        }
        protected  void addUnit(string id) {
            var d = random<UnitData>();
            d.Id = id;
            quantityDb.Units.Add(d);
            quantityDb.SaveChanges();
        }
        protected  void addOption(string id) {
            var d = random<PreferenceOptionData>();
            d.Id = id;
            db.PreferenceOptions.Add(d);
            db.SaveChanges();
        }
        protected void addContactUsage(string id) {
            var d = random<PartyContactUsageData>();
            d.PartyId = id;
            db.PartyContactUsages.Add(d);
            db.SaveChanges();
        }
    }
}
