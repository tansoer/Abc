using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abc.Pages.Parties {
    public sealed class PartyContactUsagesPage :ViewFactoryPage<PartyContactUsagesPage, IPartyContactUsagesRepo,
        PartyContactUsage, PartyContactUsageView, PartyContactUsageData, PartyContactUsageViewFactory> {
        protected override string title => PartyTitles.PartyContactUsages;
        protected internal override string subtitle => partyName(FixedValue);
        protected internal override IPageUrl masterPage() => new PartyPage(null, null, null, null, null, null, null, null, null);
        public PartyContactUsagesPage(IPartyContactUsagesRepo r) : base(r) {}
        internal IEnumerable<SelectListItem> contactUsages;
        internal IEnumerable<SelectListItem> authorities;
        internal IEnumerable<SelectListItem> parties;
        public IEnumerable<SelectListItem> ContactUsages
            => contactUsages ?? selectListByName<IClassifiersRepo, IClassifier, ClassifierData>(x => x.IsTypeOf(ClassifierType.ContactUsage));
        public IEnumerable<SelectListItem> Contacts
            => authorities ?? selectListByName<IPartyContactsRepo, IPartyContact, PartyContactData>(null, getContactText, getContactId);
        public IEnumerable<SelectListItem> Parties
            => parties ?? selectListByName<IPartyNamesRepo, PartyName, PartyNameData>(null, getPartyName, getPartyId);
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void addFields() {
            addField(x => Item.PartyId, () => Parties, () => partyName(Item.PartyId));
            addField(x => Item.Code, () => ContactUsages, () => usageText(Item.Code));
            addField(x => Item.Name, () => Contacts, () => contactText(Item.Name));
            addField(x => Item.Details);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        private static string getContactId(PartyContactData d) => d?.Id;
        private static string getPartyId(PartyNameData d) => d?.PartyId;
        private static string getContactText(PartyContactData d) => PartyContactFactory.Create(d)?.ToString();
        private static string getPartyName(PartyNameData d) => PartyNameFactory.Create(d)?.ToString();
        private string contactText(string id) => itemName(Contacts, id);
        private string usageText(string id) => itemName(ContactUsages, id);
        private string partyName(string id) => itemName(Parties, id);
        protected internal override string pageUrl => PartyUrls.PartyContactUsages;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new PartyContactUsageView();
            if (fixedFilter is null) return;
            Item.PartyId = fixedValue;
        }
        protected override void addButtons() {
            createLocalButton("Contact", Actions.Select);
            base.addButtons();
        }
        public async Task<IActionResult> OnGetSelectAsync(
           string id, string sortOrder, string searchString,
           int? pageIndex, string fixedFilter, string fixedValue) {
           Item = toView(db.Get(id));
           await Task.CompletedTask;
           return Redirect(editContactUrl(sortOrder, searchString,
               pageIndex, fixedFilter, fixedValue));
        }
        private string editContactUrl(
            string sortOrder, string searchString, 
            int? pageIndex, string fixedFilter, string fixedValue) {
            var c = new PartyContactsPage(null, null, null, null, null);
            var f = composedFixedFilter(fixedFilter);
            var args = new Args(c.PageUrl, Item?.Name, f, fixedValue, 
                sortOrder, searchString, pageIndex);
            var url = Href.Compose(args, Actions.Edit, Captions.Edit);
            url = url.Replace("<a href=\"", string.Empty);
            url = url.Replace($"\">{Captions.Edit}</a>", string.Empty);
            return url;
        }
    }
}