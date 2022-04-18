using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Abc.Aids.Constants;
using Abc.Aids.Enums;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using System;
using Abc.Domain.Common;

namespace Abc.Tests.Pages.Parties {

    [TestClass]
    public class PersonNamePrefixesPageTests :SealedViewFactoryPageTests<
        PersonNamePrefixesPage,
        INamePrefixesRepo,
        NamePrefix,
        PersonNamePrefixView,
        NamePrefixData,
        PersonNamePrefixViewFactory> {
        internal class testRepo :
            mockRepo<NamePrefix, NamePrefixData>, INamePrefixesRepo {
            public int NextIndex(string masterId) => GetRandom.UInt8();
        }
        internal class namesRepo :mockRepo<PartyName, PartyNameData>, IPartyNamesRepo { }
        internal class classificatorsRepo :mockRepo<IClassifier, ClassifierData>, IClassifiersRepo { }
        private testRepo repo;
        private namesRepo names;
        private classificatorsRepo classificators;

        protected override PersonNamePrefixesPage createObject() {
            repo = new testRepo();
            names = new namesRepo();
            classificators = new classificatorsRepo();
            sortOrder = rndStr;
            searchString = rndStr;
            pageIndex = GetRandom.UInt8();
            fixedFilter = rndStr;
            fixedValue = rndStr;
            createSwitch = GetRandom.UInt8(0, 10);
            addRandomTypes();
            addRandomNames();
            return new PersonNamePrefixesPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, names, classificators);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void addRandomNames() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var name = GetRandom.ObjectOf<PartyNameData>();
                name.PartyType = PartyType.Person;
                names.Add(new PersonName(name));
            }
        }
        private void addRandomTypes() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var t = GetRandom.ObjectOf<ClassifierData>();
                t.ClassifierType = ClassifierType.PersonNamePrefix;
                classificators.Add(ClassifierFactory.Create(t));
            }
        }
        protected override string pageTitle => PartyTitles.PersonNamePrefixes;
        protected override string pageUrl => PartyUrls.PersonNamePrefixes;
        protected override NamePrefix toObject(NamePrefixData d) => new (d);
        [TestMethod] public void PrefixesTest() => isReadOnly();
        [TestMethod] public void PartiesTest() => isReadOnly();
        [TestMethod] public void PartyNameTest() {
            areEqual(Word.Unspecified, obj.partyName(rndStr));
            var n = names.list[1];
            areEqual(n.ToString(), obj.partyName(n.Data.Id));
        }
        [TestMethod] public void PrefixNameTest() {
            areEqual(Word.Unspecified, obj.prefixName(rndStr));
            var t = classificators.list[1];
            areEqual(t.Name, obj.prefixName(t.Data.Id));
        }
    }
}
