using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Data.Classifiers;
using Abc.Data.Currencies;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Consts;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Pages.Parties {
    public class PartyContactsPage :ViewsPage<PartyContactsPage, IPartyContactsRepo, IPartyContact,
        PartyContactView, PartyContactData, ContactType> {
        internal IPartyContactUsagesRepo usages;
        internal IPartyNamesRepo names;
        internal IClassifiersRepo classifiers;
        internal ICountriesRepo countriesRepo;
        protected override string title => PartyTitles.PartyContacts;
        protected internal override string subtitle => $"{PartyName(FixedValue)}";
        protected internal override IPageUrl masterPage() => new PartyPage(null, null, null, null, null, null, null, null, null);
        public PartyContactsPage(IPartyContactsRepo e, ICountriesRepo c,
            IPartyContactUsagesRepo u, IPartyNamesRepo n, IClassifiersRepo t) : base(e) {
            countriesRepo = c;
            names = n;
            usages = u;
            classifiers = t;
        }
        internal IEnumerable<SelectListItem> contactUsages;
        internal IEnumerable<SelectListItem> countries;
        internal IEnumerable<SelectListItem> parties;
        public IEnumerable<SelectListItem> ContactUsages
            => contactUsages ?? selectListByName<IClassifiersRepo, IClassifier, ClassifierData>(  x => x.IsTypeOf(ClassifierType.ContactUsage));
        public IEnumerable<SelectListItem> Countries
            => countries ?? selectListByName<ICountriesRepo, Country, CountryData>();
        public IEnumerable<SelectListItem> Parties
            => parties ?? selectListByName<IPartyNamesRepo, PartyName, PartyNameData>(null, getName, getId);
        private static string getId(PartyNameData d) => d?.PartyId;
        private static string getName(PartyNameData d) => PartyNameFactory.Create(d)?.ToString();
        private string countryName(string id) => itemName(Countries, id);
        private string usageTypeName(string id) => itemName(ContactUsages, id);
        private string partyName(string id) => itemName(Parties, id);
        protected override void tableColumns() {
            tableColumn(x => Item.ContactType);
            tableColumn(x => Item.PartyId, () => partyName(Item.PartyId));
            tableColumn(x => Item.Contact);
            tableColumn(x => Item.ContactUsageId, () => usageTypeName(Item.ContactUsageId));
            tableColumn(x => Item.Details);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.ContactType);
            valueViewer(x => Item.PartyId, () => partyName(Item.PartyId));
            if (Item?.ContactType.IsPostal() ?? false) {
                valueViewer(x => Item.Name, PartyContactView.AddressLineCaption);
                valueViewer(x => Item.CityOrAreaCode, PartyContactView.CityCaption);
                valueViewer(x => Item.RegionOrStateOrCountryCode, PartyContactView.RegionOrStateCaption);
                valueViewer(x => Item.Code, PartyContactView.ZipOrPostCodeCaption);
                valueViewer(x => Item.CountryId, () => countryName(Item.CountryId));
            } else if (Item?.ContactType.IsTelecom() ?? false) {
                valueViewer(x => Item.RegionOrStateOrCountryCode, PartyContactView.CountryCodeCaption);
                valueViewer(x => Item.NationalDirectDialingPrefix);
                valueViewer(x => Item.CityOrAreaCode, PartyContactView.AreaCodeCaption);
                valueViewer(x => Item.Name, PartyContactView.PhoneNumberCaption);
                valueViewer(x => Item.Code, PartyContactView.ExtensionCaption);
                valueViewer(x => Item.Device);
            } else if (Item?.ContactType.IsWeb() ?? false) {
                valueViewer(x => Item.Name, PartyContactView.WebAddressCaption);
            } else if (Item?.ContactType.IsEmail() ?? false) {
                valueViewer(x => Item.Name, PartyContactView.EmailAddressCaption);
            }
            valueViewer(x => Item.ContactUsageId, () => usageTypeName(Item.ContactUsageId));
        }
        protected override void valueEditors() {
            valueEditor(x => Item.PartyId, () => Parties);
            if (Item?.ContactType.IsPostal() ?? false) {
                valueEditor(x => Item.Name, PartyContactView.AddressLineCaption);
                valueEditor(x => Item.CityOrAreaCode, PartyContactView.CityCaption);
                valueEditor(x => Item.RegionOrStateOrCountryCode, PartyContactView.RegionOrStateCaption);
                valueEditor(x => Item.Code, PartyContactView.ZipOrPostCodeCaption);
                valueEditor(x => Item.CountryId, () => Countries);
            } else if (Item?.ContactType.IsTelecom() ?? false) {
                valueEditor(x => Item.RegionOrStateOrCountryCode, PartyContactView.CountryCodeCaption);
                valueEditor(x => Item.NationalDirectDialingPrefix);
                valueEditor(x => Item.CityOrAreaCode, PartyContactView.AreaCodeCaption);
                valueEditor(x => Item.Name, PartyContactView.PhoneNumberCaption);
                valueEditor(x => Item.Code, PartyContactView.ExtensionCaption);
                valueEditor(x => Item.Device);
            } else if (Item?.ContactType.IsWeb() ?? false) {
                valueEditor(x => Item.Name, PartyContactView.WebAddressCaption);
            } else if (Item?.ContactType.IsEmail() ?? false) {
                valueEditor(x => Item.Name, PartyContactView.EmailAddressCaption);
            }
            valueEditor(x => Item.ContactUsageId, () => ContactUsages);
        }
        public override IHtmlContent EditorFor(IHtmlHelper<PartyContactsPage> h, int i) {
            if ((Item?.ContactType.IsEmail() ?? false) && (i == 1)) return emailAdrEditorFor(h, i);
            else if ((Item?.ContactType.IsWeb() ?? false) && (i == 1)) return webAdrEditorFor(h, i);
            else if (Item?.ContactType.IsTelecom() ?? false) {
                if (i == 1) return phoneNoEditorFor(h, i, 2, 3);
                else if (i == 2) return phoneNoEditorFor(h, i, 1);
                else if (i == 3) return phoneNoEditorFor(h, i, 2, 3);
                else if (i == 4) return phoneNoEditorFor(h, i, 5, 10);
                else if (i == 5) return phoneNoEditorFor(h, i, 1, 3);
            }
            return base.EditorFor(h, i);
        }
        protected internal override string pageUrl => PartyUrls.PartyContacts;
        protected internal override IPartyContact toObject(PartyContactView v)
            => new PartyContactViewFactory().Create(v);
        protected internal override PartyContactView toView(IPartyContact o) {
            var v = new PartyContactViewFactory().Create(o);
            PartyContactUsage contactUsage = getContactUsage(v.Id);
            v.ContactUsageId = contactUsage?.typeId;
            v.PartyId = contactUsage?.PartyId;
            return v;
        }
        private PartyContactUsage getContactUsage(string id) {
            PartyContactUsageData d;
            usages.FixedFilter = nameof(d.Name);
            usages.FixedValue = id;
            var list = usages.Get();
            if (list?.Count != 1) return null;
            return list[0];
        }
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new() {
                ContactType = addressType(switchOfCreate??0),
                PartyId = fixedValue
            };
        }
        protected override void doAfterOnPostCreate() {
            var forContact = usagesForContact();
            if (forContact?.Count() != 0) return;
            var u = new PartyContactUsageView {
                PartyId = Item.PartyId,
                Name = Item?.Id,
                Code = Item?.ContactUsageId,
                ValidFrom = DateTime.Now
            };
            usages.Add(new PartyContactUsageViewFactory().Create(u));
        }
        private IEnumerable<PartyContactUsage> usagesForContact() {
            var partyId = Item?.PartyId;
            var o = new PartyContactUsageView();
            var partyIdName = nameof(o.PartyId);
            if (partyId is null) return new List<PartyContactUsage>();
            var all = usages.Get(partyIdName, partyId);
            var r = all.Where(x => x.Name == Item.Id).ToList();
            return r;
        }
        protected override void doBeforeOnPostEdit() {
            var id = Guid.NewGuid().ToString();
            var forContact = usagesForContact();
            if (forContact?.Count() == 0) return;
            foreach(var usage in forContact) {
                var d = usage.Data;
                if (d is null) continue;
                d.Name = id;
                d.Code = Item.ContactUsageId;
                usages.Update(new PartyContactUsage(d));
            }
            Item.Id = id;
            db.Add(toObject(Item));
        }
        protected override void doAfterOnPostEdit() {
            if (Item?.PartyId?.IsUnspecified()?? true) return;
            var forContact = usagesForContact();
            if (forContact?.Count() != 0) return;
            var u = new PartyContactUsageView {
                PartyId = Item.PartyId,
                Name = Item?.Id,
                Code = Item?.ContactUsageId,
                ValidFrom = DateTime.Now
            };
            usages.Add(new PartyContactUsageViewFactory().Create(u));
        }
        protected override void doBeforeOnPostDelete(string id) {
            Item = toView(db.Get(id));
            var forContact = usagesForContact();
            if (forContact?.Count() == 0) return;
            foreach (var usage in forContact) usages.Delete(usage.Id);
        }
        internal static ContactType addressType(int i) =>
            Enum.IsDefined(typeof(ContactType), i)
            ? (ContactType)i
            : ContactType.Unspecified;
        private string PartyName(string partyId) {
            var partyNames = names.Get(nameof(PersonName.PartyId), partyId);
            if (partyNames?.Count < 0) return null;
            var name = partyNames[0];
            return name.ToString();
        }
        public override async Task<IActionResult> OnGetIndexAsync(string sortOrder,
        string id, string currentFilter, string searchString,
        int? pageIndex, string fixedFilter, string fixedValue, string[] fixedValues = null) {
            string url = null;
            if (isRedirect(fixedFilter)) 
                url = redirectUrl(sortOrder, id, currentFilter, searchString, pageIndex, fixedFilter, fixedValue);
            if (url != null) return Redirect(url);
            return await base.OnGetIndexAsync(sortOrder, id, currentFilter, 
                searchString, pageIndex, fixedFilter, fixedValue, fixedValues);
        }
        private string redirectUrl(string sortOrder, string id, string currentFilter, string searchString, int? pageIndex, string fixedFilter, string fixedValue) {
            var page = fixedFilter.GetHead();
            if (!nameof(PartyContactUsagesPage).StartsWith(page)) return null;
            var c = new PartyContactUsagesPage(null);
            var args = new Args(c.PageUrl, Item?.Name, fixedFilter.GetTail(), fixedValue,
                sortOrder, searchString, pageIndex);
            var url = Href.Compose(args, Actions.Index, Captions.Edit);
            url = url.Replace("<a href=\"", string.Empty);
            url = url.Replace($"\">{Captions.Edit}</a>", string.Empty);
            return url;
        }
        private static bool isRedirect(string s) => s.GetTail() != s.GetHead();
    }
}
