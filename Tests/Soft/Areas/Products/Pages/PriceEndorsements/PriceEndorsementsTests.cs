using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Facade.Products;
using Abc.Infra.Parties;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.PriceEndorsements {
    public abstract class PriceEndorsementsTests :BaseProductTests<PriceEndorsementView, PriceEndorsementData> {
        protected List<SelectListItem> prices => createSelectList(db.Prices);
        protected List<SelectListItem> partySignatures {
            get => getContext<PartyDb>().PartySignatures.Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        }
        protected override string baseUrl() => ProductUrls.PriceEndorsements;
        protected override PriceEndorsementView toView(PriceEndorsementData d) 
            => new PriceEndorsementViewFactory().Create(new Abc.Domain.Products.Price.PriceEndorsement(d));
        protected override void changeValuesExceptId(PriceEndorsementData d) {
            var id = d.Id;
            d = random<PriceEndorsementData>();
            d.Id = id;
        }
        protected override string getItemId(PriceEndorsementData d) => d.Id;
        protected override void setItemId(PriceEndorsementData d, string id) => d.Id = id;
        protected override void addRelatedItems(PriceEndorsementData d) {
            if (d is null) return;
            addPrice(d.PriceId);
            addPartySignature(d.PartySignatureId);
        }
        protected override IEnumerable<Expression<Func<PriceEndorsementView, object>>> indexPageColumns =>
            new Expression<Func<PriceEndorsementView, object>>[] {
                x => x.PriceId,
                x => x.PartySignatureId,
                x => x.Name,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.PriceId, Unspecified.String);
            canView(item, m => m.PartySignatureId, Unspecified.String);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canSelect(item, m => m.PriceId, prices);
            canSelect(item, m => m.PartySignatureId, partySignatures);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canSelect(null, x => x.PriceId, prices);
            canSelect(null, x => x.PartySignatureId, partySignatures);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :PriceEndorsementsTests { }
    [TestClass] public class DeletePageTests :PriceEndorsementsTests { }
    [TestClass] public class DetailsPageTests :PriceEndorsementsTests { }
    [TestClass] public class EditPageTests :PriceEndorsementsTests { }
    [TestClass] public class IndexPageTests :PriceEndorsementsTests { }
}
