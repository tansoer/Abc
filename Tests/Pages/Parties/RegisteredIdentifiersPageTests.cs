using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Identifiers;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Parties {
    [TestClass] public class RegisteredIdentifiersPageTests :SealedViewFactoryPageTests<
        RegisteredIdentifiersPage, 
        IRegisteredIdentifiersRepo, 
        RegisteredIdentifier, 
        RegisteredIdentifierView, 
        RegisteredIdentifierData, 
        RegisteredIdentifierViewFactory> {
        private testRepo Repo;
        private namesRepo names;
        private classificatorsRepo classificators;
        internal class testRepo :mockRepo<RegisteredIdentifier, RegisteredIdentifierData>, IRegisteredIdentifiersRepo { }
        internal class namesRepo :mockRepo<PartyName, PartyNameData>, IPartyNamesRepo { }
        internal class classificatorsRepo :mockRepo<IClassifier, ClassifierData>, IClassifiersRepo { }

        protected override RegisteredIdentifier toObject(RegisteredIdentifierData d) => new (d);
 
        protected override RegisteredIdentifiersPage createObject() {
            Repo = new testRepo();
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
            return new RegisteredIdentifiersPage(Repo, names, classificators);
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
                t.ClassifierType = ClassifierType.RegisteredIdentifier;
                classificators.Add(ClassifierFactory.Create(t));
            }
        }
        protected override string pageTitle => PartyTitles.RegisteredIdentifiers;
        protected override string pageUrl => PartyUrls.RegisteredIdentifiers;

        [TestMethod] public void IdentifiersTest() => isReadOnly();
        [TestMethod] public void AuthoritiesTest() => isReadOnly();
        [TestMethod] public void PartiesTest() => isReadOnly();
    }
}
