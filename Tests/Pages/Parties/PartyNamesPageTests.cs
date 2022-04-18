using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Classifiers;
using Abc.Pages.Common;
using Abc.Pages.Parties;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Pages.Parties {
    [TestClass] public class PartyNamesPageTests :SealedViewsFactoryPageTests<
        PartyNamesPage,
        IPartyNamesRepo,
        PartyName,
        PartyNameView,
        PartyNameData,
        PartyNameViewFactory,
        PartyType> {
        private class testRepo : mockRepo<PartyName, PartyNameData>,
                IPartyNamesRepo {
        }
        private class testPartiesRepo : mockRepo<IParty, PartyData>, IPartiesRepo { }

        private testRepo Repo;
        private testPartiesRepo partiesRepo;

        protected override PartyNamesPage createObject() {
            Repo = new testRepo();
            partiesRepo = new testPartiesRepo();
            addRandomTypes();
            return new PartyNamesPage(Repo, partiesRepo);
        }

        private void addRandomTypes() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var newName = GetRandom.ObjectOf<PartyNameData>();
                newName.PartyType = PartyType.Person;
                Repo.Add(new PersonName(newName));
            }
        }
        protected override string pageTitle => PartyTitles.Names;
        protected override string pageUrl => PartyUrls.Names;
        protected override PartyName toObject(PartyNameData d) => new PersonName(d);

        [TestMethod] public override void ToObjectTest() {
            var view = GetRandom.ObjectOf<PartyNameView>();
            var o = obj.toObject(view);
            allPropertiesAreEqual(o.Data, view);
        }
        [TestMethod] public override void ToViewTest() {
            var o = GetRandom.ObjectOf<PersonName>();
            var view = obj.toView(o);
            allPropertiesAreEqual(o.Data, view);
        }
    }
}