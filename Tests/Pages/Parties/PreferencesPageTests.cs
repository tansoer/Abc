using Abc.Aids.Constants;
using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Data.Quantities;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Preferences;
using Abc.Domain.Quantities;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Pages.Parties {
    [TestClass] public class PreferencesPageTests :SealedViewFactoryPageTests<
        PreferencesPage, 
        IPreferencesRepo,
        Preference,
        PreferenceView, 
        PreferenceData, 
        PreferenceViewFactory> {
        private class testRepo :mockRepo<Preference, PreferenceData>, IPreferencesRepo { }
        internal class partiesRepo :mockRepo<IParty, PartyData>, IPartiesRepo { }
        internal class classifiersRepo :mockRepo<IClassifier, ClassifierData>, IClassifiersRepo { }
        internal class unitsRepo :mockRepo<Unit, UnitData>, IUnitsRepo { }
        internal class optionsRepo :mockRepo<PreferenceOption, PreferenceOptionData>, IPreferenceOptionsRepo { }

        private testRepo preferences;
        private partiesRepo parties;
        private classifiersRepo classifiers;
        private unitsRepo units;
        private optionsRepo options;
        private void addRandomParties() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var party = GetRandom.ObjectOf<PartyData>();
                parties.Add(PartyFactory.Create(party));
            }
        }
        private void addRandomTypes() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var t = GetRandom.ObjectOf<ClassifierData>();
                t.ClassifierType = ClassifierType.PartyPreference;
                classifiers.Add(ClassifierFactory.Create(t));
            }
        }
        private void addRandomUnits() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var unit = GetRandom.ObjectOf<UnitData>();
                units.Add(UnitFactory.Create(unit));
            }
        }
        private void addRandomOptions() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var option = GetRandom.ObjectOf<PreferenceOptionData>();
                options.Add(new PreferenceOption(option));
            }
        }
        protected override Preference toObject(PreferenceData d) => new (d);
        protected override PreferencesPage createObject() {
            preferences = new testRepo();
            parties = new partiesRepo();
            classifiers = new classifiersRepo();
            units = new unitsRepo();
            options = new optionsRepo();
            addRandomParties();
            addRandomTypes();
            addRandomUnits();
            addRandomOptions();
            return new PreferencesPage(preferences); 
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(preferences, parties, classifiers,units, options);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        protected override string pageTitle => PartyTitles.Preferences;
        protected override string pageUrl => PartyUrls.Preferences;
        [TestMethod] public void PartiesTest() => isReadOnly(obj.Parties);
        [TestMethod] public void PreferenceTypesTest() => isReadOnly(obj.PreferenceTypes);
        [TestMethod] public void UnitsTest() => isReadOnly(obj.Units);
        [TestMethod] public void OptionsTest() => isReadOnly(obj.Options);
        [TestMethod] public void PartyNameTest() {
            Assert.AreEqual(Word.Unspecified, obj.PartyName(rndStr));
            var n = parties.list[1];
            Assert.AreEqual(n.GetName(), obj.PartyName(n.Data.Id));
        }
        [TestMethod] public void PreferenceNameTest() {
            Assert.AreEqual(Word.Unspecified, obj.PreferenceName(rndStr));
            var t = classifiers.list[1];
            Assert.AreEqual(t.Name, obj.PreferenceName(t.Data.Id));
        }
        [TestMethod] public void UnitNameTest() {
            Assert.AreEqual(Word.Unspecified, obj.UnitName(rndStr));
            var u = units.list[1];
            Assert.AreEqual(u.Name, obj.UnitName(u.Data.Id));
        }
        [TestMethod] public void OptionNameTest() {
            Assert.AreEqual(Word.Unspecified, obj.OptionName(rndStr));
            var o = options.list[1];
            Assert.AreEqual(o.Name, obj.OptionName(o.Data.Id));
        }
    }
}
