using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Facade.Parties;
using Abc.Pages.Classifiers;
using Abc.Pages.Parties;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Parties {
    [TestClass]
    public class PersonEthnicitiesPageTests :SealedViewFactoryPageTests<
        PersonEthnicitiesPage, 
        IPersonEthnicitiesRepo,
        PersonEthnicity, 
        PersonEthnicityView, 
        PersonEthnicityData, 
        PersonEthnicityViewFactory> {
        protected override PersonEthnicity toObject(PersonEthnicityData d) => new (d);

        protected override PersonEthnicitiesPage createObject() {
            ethnicities = new ethnicitiesRepo();
            persons = new personsRepo();
            classifiers = new classifiersRepo();
            addRandomEthnicities();
            addRandomPersons();
            addRandomClassifiers();
            return new PersonEthnicitiesPage(ethnicities, persons, classifiers);
        }

        private class ethnicitiesRepo :mockRepo<PersonEthnicity, PersonEthnicityData>, IPersonEthnicitiesRepo { }
        private class personsRepo :mockRepo<IParty, PartyData>, IPartiesRepo { }
        private class classifiersRepo :mockRepo<IClassifier, ClassifierData>, IClassifiersRepo { }

        private ethnicitiesRepo ethnicities;
        private personsRepo persons;
        private classifiersRepo classifiers;

        protected override string pageTitle => PartyTitles.PersonEthnicities;
        protected override string pageUrl => PartyUrls.PersonEthnicities;

        private void addRandomEthnicities() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                ethnicities.Add(new PersonEthnicity(GetRandom.ObjectOf<PersonEthnicityData>()));
        }

        private void addRandomPersons() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                persons.Add(new Person(GetRandom.ObjectOf<PartyData>()));
        }

        private void addRandomClassifiers() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var d = GetRandom.ObjectOf<ClassifierData>();
                d.ClassifierType = ClassifierType.PartyEthnicity;
                classifiers.Add(new Ethnicity(d));
            }
        }

        [TestMethod] public void EthnicitiesTest() => isReadOnly(obj.Ethnicities);
        [TestMethod] public void PersonsTest() => isReadOnly(obj.Persons);

        [TestMethod]
        public void EthnicityNameTest() {
            var rnd = GetRandom.Int32(0, obj.Ethnicities.ToList().Count - 1);
            var expected = obj.Ethnicities.ToList()[rnd];
            areEqual(expected.Text, obj.EthnicityName(expected.Value));
        }

        [TestMethod]
        public void PersonNameTest() {
            var rnd = GetRandom.Int32(0, obj.Persons.ToList().Count - 1);
            var expected = obj.Persons.ToList()[rnd];
            areEqual(expected.Text, obj.PersonName(expected.Value));
        }
    }
}
