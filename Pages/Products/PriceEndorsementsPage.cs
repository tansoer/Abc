using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Products.Price;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Products {
    public sealed class PriceEndorsementsPage :ViewPage<PriceEndorsementsPage, IPriceEndorsementsRepo, PriceEndorsement,
        PriceEndorsementView, PriceEndorsementData> {
        protected override string title => ProductTitles.PriceEndorsements;
        private IEnumerable<SelectListItem> prices;
        private IEnumerable<SelectListItem> partySignatures;
        public IEnumerable<SelectListItem> Prices => prices ??= selectListByName<IPricesRepo, IPrice, PriceData>();
        public IEnumerable<SelectListItem> PartySignatures => partySignatures ??= selectListByName<IPartySignaturesRepo, PartySignature, PartySignatureData>();
        public PriceEndorsementsPage(IPriceEndorsementsRepo r) :base(r) {}
        protected override void tableColumns() {
            tableColumn(x => Item.PriceId);
            tableColumn(x => Item.PartySignatureId);
            tableColumn(x => Item.Name);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        public override IHtmlContent ValueFor(IHtmlHelper<PriceEndorsementsPage> html, int i) => i switch {
            0 => html.Raw(PriceName(Item?.PriceId)),
            1 => html.Raw(PartySignatureName(Item?.PartySignatureId)),
            _ => base.ValueFor(html, i)
        };
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.PriceId, () => PriceName(Item.PriceId));
            valueViewer(x => Item.PartySignatureId, () => PartySignatureName(Item.PartySignatureId));
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.PriceId, () => Prices);
            valueEditor(x => Item.PartySignatureId, () => PartySignatures);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProductUrls.PriceEndorsements;
        protected internal override PriceEndorsement toObject(PriceEndorsementView v)
            => new PriceEndorsementViewFactory().Create(v);
        protected internal override PriceEndorsementView toView(PriceEndorsement o)
            => new PriceEndorsementViewFactory().Create(o);
        public override IActionResult OnGetCreate(
            string sortOrder, string searchString, int? pageIndex,
            string fixedFilter, string fixedValue, int? switchOfCreate) {
            createItem();
            return base.OnGetCreate(sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue, switchOfCreate);
        }
        private void createItem() => Item = new();
        public string PriceName(string id) => itemName(Prices, id);
        public string PartySignatureName(string id) => itemName(PartySignatures, id);
    }
}
