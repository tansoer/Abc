using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Parties {
    [TestClass]
    public class PartyContactUsagesPageTests :SealedViewFactoryPageTests<
        PartyContactUsagesPage, 
        IPartyContactUsagesRepo,
        PartyContactUsage, 
        PartyContactUsageView, 
        PartyContactUsageData, 
        PartyContactUsageViewFactory> {
        private testRepo Repo;
        private namesRepo names;
        private classificatorsRepo classificators;
        private contactsRepo contacts;
        internal class testRepo : mockRepo<PartyContactUsage, PartyContactUsageData>, IPartyContactUsagesRepo { }
        internal class namesRepo : mockRepo<PartyName, PartyNameData>, IPartyNamesRepo { }
        internal class classificatorsRepo : mockRepo<IClassifier, ClassifierData>, IClassifiersRepo { }
        internal class contactsRepo : mockRepo<IPartyContact, PartyContactData>, IPartyContactsRepo { }
        protected override PartyContactUsage toObject(PartyContactUsageData d) => new(d);
        protected override PartyContactUsagesPage createObject() {
            Repo = new testRepo();
            names = new namesRepo();
            classificators = new classificatorsRepo();
            contacts = new contactsRepo();
            sortOrder = rndStr;
            searchString = rndStr;
            pageIndex = GetRandom.UInt8();
            fixedFilter = rndStr;
            fixedValue = rndStr;
            createSwitch = GetRandom.UInt8(0, 10);
            addRandomTypes();
            addRandomNames();
            addRandomContacts();
            return new PartyContactUsagesPage(Repo);
        }
        private void addRandomNames() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var d = random<PartyNameData>();
                names.Add(PartyNameFactory.Create(d));
            }
        }
        private void addRandomTypes() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var t = GetRandom.ObjectOf<ClassifierData>();
                t.ClassifierType = ClassifierType.ContactUsage;
                classificators.Add(ClassifierFactory.Create(t));
            }
        }
        private void addRandomContacts() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var t = GetRandom.ObjectOf<PartyContactData>();
                contacts.Add(PartyContactFactory.Create(t));
            }
        }
        protected override string pageTitle => PartyTitles.PartyContactUsages;
        protected override string pageUrl => PartyUrls.PartyContactUsages;

        [TestMethod] public void ContactUsagesTest() => isReadOnly();
        [TestMethod] public void ContactsTest() => isReadOnly();
        [TestMethod] public void PartiesTest() => isReadOnly();
    }
}
