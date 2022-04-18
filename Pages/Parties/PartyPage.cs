using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Enums;
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
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Parties {
    public sealed class PartyPage : ViewsFactoryPage<PartyPage, IPartiesRepo, IParty, PartyView, PartyData, PartyViewFactory, PartyType> {
        internal IPartyNamesRepo names;
        internal IPartyContactUsagesRepo usages;
        internal IPartyContactsRepo contacts;
        internal IBodyMetricsRepo metrics;
        protected override string title => PartyTitles.Parties;
        public PartyPage(IPartiesRepo r, IPartyNamesRepo n, IClassifiersRepo t, IPartyContactUsagesRepo u, 
            IPartyContactsRepo c, ICountriesRepo s, IBodyMetricsRepo b, IBodyMetricTypesRepo bt, IUnitsRepo un): base(r) {
            names = n;
            Names = names?.Get();
            OrganizationTypes = selectListByName<IClassifiersRepo, IClassifier, ClassifierData>(isOrganization);
            usages = u;
            ContactUsages = usages?.Get();
            contacts = c;
            Contacts = contacts?.Get();
            Countries = selectListByName<ICountriesRepo, Country, CountryData>();
            metrics = b;
            BodyMetrics = metrics?.Get();
            BodyMetricTypes = selectListByName<IBodyMetricTypesRepo, BodyMetricType, BodyMetricTypeData>();
            Units = selectListByName<IUnitsRepo, Unit, UnitData>();
        }
        internal static bool isOrganization(IClassifier x) => x?.Data?.ClassifierType == ClassifierType.Organization;
        public IEnumerable<IPartyName> Names { get; }
        public IEnumerable<SelectListItem> OrganizationTypes { get; }
        public IEnumerable<SelectListItem> Countries { get; }
        public IEnumerable<PartyContactUsage> ContactUsages { get; }
        public IEnumerable<IPartyContact> Contacts { get; }
        public IEnumerable<IBodyMetric> BodyMetrics { get; set; }
        public IEnumerable<SelectListItem> Genders = new SelectList(Enum.GetValues<IsoGender>().ToList());
        [BindProperty] public PartyNameView Name { get; set; }
        [BindProperty] public List<PartyContactView> PartyContacts { get; set; }
        [BindProperty] public List<BodyMetricView> Metrics { get; set; }
        public IEnumerable<SelectListItem> BodyMetricTypes { get; set; }
        public IEnumerable<SelectListItem> Units { get; set; }
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.PartyType),
            field(x => Item.Id),
            field(x => Item.Name),
            field(x => Name.Id),
            field(x => Name.PartyId),
            field(x => Name.PartyType),
            field(x => Name.NameType)
        };
        protected override void addFields() {
            addField(x => Item.PartyType);
            addField(x => Item.Name);
            addField(x => Item.Description);
        }
        protected override List<ExpressionHelper> getFieldsForViewers() {
            var helpers = Helpers.GetRange(0, 3);
            if (Item.IsPerson()) {
                helpers.Add(field(x => Item.ValidFrom, "Date Of Birth"));
                helpers.Add(field(x => Item.Gender, () => Genders));
            }
            else if (Item.IsOrganization()) helpers.Add(field(x => Item.OrganizationTypeId, () => OrganizationTypes));
            return helpers;
        }
        protected override List<ExpressionHelper> getFieldsForEditors() {
            var helpers = new List<ExpressionHelper>();
            if (Item.IsPerson()) {
                helpers.Add(field(x => Name.Name, "Family Name"));
                helpers.Add(field(x => Name.GivenName));
                helpers.Add(field(x => Name.MiddleName));
                helpers.Add(field(x => Name.PreferredName));
                helpers.AddRange(getFieldsForViewers().GetRange(2, 3));
            }
            else if (Item.IsOrganization()) {
                helpers.AddRange(getFieldsForViewers().GetRange(1, 3));
                helpers[0] = field(x => Name.Name);
            } else helpers.Add(field(x => Name.Name));
            return helpers;
        }
        protected internal override string pageUrl => PartyUrls.Parties;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => createItems(switchOfCreate ?? 0);
        protected override void doBeforeOnGetEdit(string id) {
            var use = ContactUsages.Where(x => x.PartyId == id);
            PartyContacts = new List<PartyContactView>();
            foreach (var u in use) PartyContacts.Add(new PartyContactViewFactory().Create(Contacts.FirstOrDefault(x => x.Id == u.Name)));
            Metrics = new List<BodyMetricView>();
            foreach (var m in metrics.Get().ToList().Where(x => x.Data.PartyId == id)) Metrics.Add(new BodyMetricViewFactory().Create(m));
            GetName(id);
        }

        protected override void doAfterOnPostCreate() => CreatePersonsRelatedData();
        protected override void doAfterOnPostDelete(string id) => DeletePersonsRelatedData(id);
        protected override void doAfterOnPostEdit() => UpdateOrCreatePersonRelatedData();

        private void CreatePersonsRelatedData() {
            names.Add(new PartyNameViewFactory().Create(Name));
            foreach (var contact in PartyContacts) {
                contacts.Add(new PartyContactViewFactory().Create(contact));
                usages.Add(new PartyContactUsage(new PartyContactUsageData
                    {PartyId = Item.Id, Name = contact.Id}));
            }
            foreach (var metric in Metrics)
                metrics.Add(new BodyMetricViewFactory().Create(metric));
        }
        public void UpdatePersonsRelatedData(IPartyNamesRepo repo) 
            => repo.Update(new PartyNameViewFactory().Create(Name));
        private void DeletePersonsRelatedData(string partyId) {
            var relatedNames = Names.Where(x => x.Data.PartyId == partyId);
            foreach (var relatedName in relatedNames) names.Delete(relatedName.Data.Id);

            var relatedContactUsages = ContactUsages.Where(x => x.PartyId == partyId);
            foreach (var relatedUsage in relatedContactUsages) {
                usages.Delete(relatedUsage.Data.Id);
                var contact = Contacts.FirstOrDefault(x => x.Id == relatedUsage.Data.Name);
                if (ContactUsages.Count(x => x.Data.Name == contact?.Id) < 2) contacts.Delete(contact?.Id);
            }

            var relatedBodyMetrics = metrics.Get().ToList().Where(x => x.Data.PartyId == partyId);
            foreach (var relatedMetric in relatedBodyMetrics) metrics.Delete(relatedMetric.Data.Id);
        }
        private void UpdateOrCreatePersonRelatedData() {
            Name.PartyType = Item.PartyType;
            if (Names.Any(x => x.Id == Name.Id)) UpdatePersonsRelatedData(names);
            else CreatePersonsRelatedData();

            ModifyContacts();
            ModifyMetrics();
        }

        private void ModifyMetrics() {
            foreach (var original in BodyMetrics.Where(x => x.Data.PartyId == Item.Id).ToList()) {
                var m = Metrics.FirstOrDefault(x => x.Id == original.Id);
                if (m == null) metrics.Delete(original.Id);
                else updateMetric(m);
            }

            foreach (var m in Metrics) metrics.Add(new BodyMetricViewFactory().Create(m));
        }

        private void updateMetric(BodyMetricView m) {
            metrics.Update(new BodyMetricViewFactory().Create(m));
            Metrics.Remove(m);
        }

        private void ModifyContacts() {
            var originalContacts = ContactUsages
                .Where(x => x.PartyId == Item.Id)
                .Select(u => new PartyContactViewFactory().Create
                (Contacts.FirstOrDefault(x => x.Id == u.Name))).ToList();

            foreach (var t in originalContacts){
                var c = PartyContacts.FirstOrDefault(x => x.Id == t.Id);
                if (c == null) deleteContact(t);
                else updateContact(c);
            }

            foreach (var contact in PartyContacts) {
                contacts.Add(new PartyContactViewFactory().Create(contact));
                usages.Add(new PartyContactUsage(new PartyContactUsageData { PartyId = Item.Id, Name = contact.Id }));
            }
        }

        private void updateContact(PartyContactView contact) {
            var uses = ContactUsages.Where(x => x.Name == contact.Id).ToList();
            if (uses.Count == 1) contacts.Update(new PartyContactViewFactory().Create(contact));
            var newUsage = new PartyContactUsageViewFactory().Create(uses.FirstOrDefault(x => x.PartyId == Item.Id));
            newUsage.Name = Guid.NewGuid().ToString();
            usages.Update(new PartyContactUsageViewFactory().Create(newUsage));
            contact.Id = newUsage.Name;
            contacts.Add(new PartyContactViewFactory().Create(contact));
            PartyContacts.Remove(contact);
        }

        private void deleteContact(PartyContactView contact) {
            var uses = ContactUsages.Where(x => x.Name == contact.Id).ToList();
            var currentUsage = uses.FirstOrDefault(x => x.PartyId == Item.Id);
            usages.Delete(currentUsage?.Id);
            if (uses.Count == 1) contacts.Delete(currentUsage?.Name);
        }

        private void createItems(int switchOfCreate) {
            Item = new() {
                Id = Guid.NewGuid().ToString(),
                PartyType = partyType(switchOfCreate)
            };
            Name = createName(Item.Id, switchOfCreate);
            PartyContacts = new();
            Metrics = new();
        }
        internal static PartyNameView createName(string id, int switchOfCreate = 0) => new() {
            Id = Guid.NewGuid().ToString(),
            PartyId = id,
            PartyType = partyType(switchOfCreate),
            NameType = NameType.Official
        };
        internal static PartyType partyType(int i) =>
            Enum.IsDefined(typeof(PartyType), i)
                ? (PartyType)i
                : PartyType.Unspecified;
        internal PartyNameView GetName(string id) => Name = getName(id).PartyId != null ? Name : createName(id);
        private PartyNameView getName(string id) => 
            Name = new PartyNameViewFactory().Create(
                PartyNameFactory.Create(names.Get().FirstOrDefault(
                    x => x.PartyId == id && x.NameType == NameType.Official)?.Data));
    }
}