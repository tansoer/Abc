using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Parties;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Abc.Aids.Constants;
using Abc.Aids.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;

namespace Abc.Tests.Pages.Parties {
    [TestClass] public class PersonNameSuffixesPageTests :SealedViewFactoryPageTests<
        PersonNameSuffixesPage,
        INameSuffixesRepo,
        NameSuffix,
        PersonNameSuffixView,
        NameSuffixData,
        PersonNameSuffixViewFactory> {
        internal class testRepo : mockRepo<NameSuffix, NameSuffixData>, INameSuffixesRepo {
            public int NextIndex(string masterId) => GetRandom.UInt8();
        }
        internal class namesRepo :mockRepo<PartyName, PartyNameData>, IPartyNamesRepo { }
        internal class classifiersRepo :mockRepo<IClassifier, ClassifierData>, IClassifiersRepo { }
        private testRepo repo;
        private namesRepo names;
        private classifiersRepo classifiers;
        protected override PersonNameSuffixesPage createObject() {
            repo = new testRepo();
            names = new namesRepo();
            classifiers = new classifiersRepo();
            sortOrder = rndStr;
            searchString = rndStr;
            pageIndex = GetRandom.UInt8();
            fixedFilter = rndStr;
            fixedValue = rndStr;
            createSwitch = GetRandom.UInt8(0, 10);
            addRandomNames();
            addRandomTypes();
            return new PersonNameSuffixesPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, names, classifiers);
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
                t.ClassifierType = ClassifierType.PersonNameSuffix;
                classifiers.Add(ClassifierFactory.Create(t));
            }
        }
        protected override string pageUrl => PartyUrls.PersonNameSuffixes;
        protected override string pageTitle => PartyTitles.PersonNameSuffixes;
        protected override NameSuffix toObject(NameSuffixData d) => new (d);
        [TestMethod] public void SuffixesTest() => isReadOnly();
        [TestMethod] public void PartiesTest() => isReadOnly();
        [TestMethod] public void PartyNameTest() {
            Assert.AreEqual(Word.Unspecified, obj.partyName(rndStr));
            var n = names.list[1];
            Assert.AreEqual(n.ToString(), obj.partyName(n.Data.Id));
        }
        [TestMethod] public void SuffixNameTest() {
            Assert.AreEqual(Word.Unspecified, obj.suffixName(rndStr));
            var t = classifiers.list[1];
            Assert.AreEqual(t.Name, obj.suffixName(t.Data.Id));
        }
    }
}
