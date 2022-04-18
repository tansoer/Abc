using System;
using System.Threading.Tasks;
using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Currencies;
using Abc.Data.Parties;
using Abc.Data.Quantities;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Parties.Names;
using Abc.Domain.Quantities;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Parties;
using Abc.Tests.Pages.Common;
using AngleSharp.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Parties {
    [TestClass] public class PartyPageTests : SealedViewsFactoryPageTests<
        PartyPage, 
        IPartiesRepo,
        IParty, 
        PartyView, 
        PartyData, 
        PartyViewFactory,
        PartyType> {
        private class testRepo : mockRepo<IParty, PartyData>, IPartiesRepo {}
        private class testPartyNamesRepo : mockRepo<PartyName, PartyNameData>, IPartyNamesRepo { }
        private class testOrganizationTypesRepo : mockRepo<IClassifier, ClassifierData>, IClassifiersRepo { }
        private class testPartyContactUsagesRepo : mockRepo<PartyContactUsage, PartyContactUsageData>, IPartyContactUsagesRepo { }
        private class testPartyContactsRepo : mockRepo<IPartyContact, PartyContactData>, IPartyContactsRepo { }
        private class testCountriesRepo : mockRepo<Country, CountryData>, ICountriesRepo { }
        private class testBodyMetricsRepo : mockRepo<IBodyMetric, BodyMetricData>, IBodyMetricsRepo { }
        private class testBodyMetricTypesRepo : mockRepo<BodyMetricType, BodyMetricTypeData>, IBodyMetricTypesRepo { }
        private class testUnitsRepo : mockRepo<Unit, UnitData>, IUnitsRepo { }

        private testRepo Repo;
        private testPartyNamesRepo partyNamesRepo;
        private testOrganizationTypesRepo organizationTypesRepo;
        private testPartyContactUsagesRepo contactUsagesRepo;
        private testPartyContactsRepo contactsRepo;
        private testCountriesRepo countriesRepo;
        private testBodyMetricsRepo bodyMetricsRepo;
        private testBodyMetricTypesRepo bodyMetricTypesRepo;
        private testUnitsRepo unitsRepo;

        protected override PartyPage createObject() {
            Repo = new testRepo();
            partyNamesRepo = new testPartyNamesRepo();
            organizationTypesRepo = new testOrganizationTypesRepo();
            contactUsagesRepo = new testPartyContactUsagesRepo();
            contactsRepo = new testPartyContactsRepo();
            countriesRepo = new testCountriesRepo();
            bodyMetricsRepo = new testBodyMetricsRepo();
            bodyMetricTypesRepo = new testBodyMetricTypesRepo();
            unitsRepo = new testUnitsRepo();
            return new PartyPage(Repo, partyNamesRepo, organizationTypesRepo, contactUsagesRepo, contactsRepo, countriesRepo, bodyMetricsRepo, bodyMetricTypesRepo, unitsRepo);
        }
        protected override string pageTitle => PartyTitles.Parties;

        protected override string pageUrl => PartyUrls.Parties;

        protected override IParty toObject(PartyData d) => new Person(d);

        [TestMethod] public void NamesTest() => isReadOnly(obj.Names);
        [TestMethod] public void OrganizationTypesTest() => isReadOnly(obj.OrganizationTypes);
        [TestMethod] public void ContactUsagesTest() => isReadOnly(obj.ContactUsages);
        [TestMethod] public void ContactsTest() => isReadOnly(obj.Contacts);
        [TestMethod] public void NameTest() => isNullable<PartyNameView>();

        [TestMethod] public void OnPostEditAsyncTest() {
            obj.Item = random<PartyView>();
            obj.Name = random<PartyNameView>();
            var page = obj.OnPostEditAsync(sortOrder, searchString, pageIndex, fixedFilter, fixedValue);
            isInstanceOfType(page, typeof(Task<IActionResult>));
        }

        [TestMethod] public void OnPostDeleteAsyncTest() {
            var randomItem = GetRandom.ObjectOf<Person>();
            Repo.Add(randomItem);
            isTrue(Repo.list.Contains(randomItem));
            var page = obj.OnPostDeleteAsync(randomItem.Id, sortOrder, searchString, pageIndex, fixedFilter, fixedValue);
            isFalse(Repo.list.Contains(randomItem));
        }

        [TestMethod] public void UpdatePersonsRelatedDataTest() {
            obj.Item = GetRandom.ObjectOf<PartyView>();
            var rndName = GetRandom.ObjectOf<PartyNameData>();
            rndName.PartyId = obj.Item.Id;
            var rndNameObj = new PersonName(rndName);
            partyNamesRepo.Add(rndNameObj);
            obj.Name = new PartyNameViewFactory().Create(rndNameObj);
            obj.Name.Name = rndStr;
            obj.Name.GivenName = rndStr;
            obj.Name.MiddleName = rndStr;
            obj.Name.PartyType = Abc.Aids.Enums.PartyType.Person;
            obj.UpdatePersonsRelatedData(partyNamesRepo);
            var repoObj = partyNamesRepo.Get(rndName.Id);
            areEqual(repoObj.ToDictionary()["Name"], obj.Name.Name);
            areEqual(repoObj.ToDictionary()["GivenName"], obj.Name.GivenName);
            areEqual(repoObj.ToDictionary()["MiddleName"], obj.Name.MiddleName);
        }
        [TestMethod] public override void ToObjectTest() {
            var view = GetRandom.ObjectOf<PartyView>();
            var o = obj.toObject(view);
            allPropertiesAreEqual(o.Data, view, "OrganizationTypeId");
        }
        [TestMethod] public override void ToViewTest() {
            var o = GetRandom.ObjectOf<Person>();
            var view = obj.toView(o);
            allPropertiesAreEqual(o.Data, view, "OrganizationTypeId", "Name");
        }
    }
}