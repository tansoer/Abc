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

namespace Abc.Tests.Soft.Areas.Products.Pages.BatchCheckedBy {
    public abstract class BatchCheckedByTests :BaseProductTests<BatchCheckedByView, BatchCheckedByData> {
        protected List<SelectListItem> batches => createSelectList(db.Batches);
        protected List<SelectListItem> partySignatures {
            get => getContext<PartyDb>().PartySignatures.Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        }
        protected override string baseUrl() => ProductUrls.BatchCheckedBy;
        protected override BatchCheckedByView toView(BatchCheckedByData d)
            => new BatchCheckedByViewFactory().Create(new Abc.Domain.Products.BatchCheckedBy(d));
        protected override string baseClassName() => "BatchCheckedBy";
        protected override void changeValuesExceptId(BatchCheckedByData d) {
            var id = d.Id;
            d = random<BatchCheckedByData>();
            d.Id = id;
        }
        protected override string getItemId(BatchCheckedByData d) => d.Id;
        protected override void setItemId(BatchCheckedByData d, string id) => d.Id = id;
        protected override string performTitleCorrection(string title) => title;
        protected override void addRelatedItems(BatchCheckedByData d) {
            if (d is null) return;
            addBatch(d.BatchId);
            addPartySignature(d.PartySignatureId);
        }
        protected override IEnumerable<Expression<Func<BatchCheckedByView, object>>> indexPageColumns =>
            new Expression<Func<BatchCheckedByView, object>>[] {
                x => x.BatchId,
                x => x.PartySignatureId,
                x => x.CheckedBy,
                x => x.Address,
                x => x.EmailAddress,
                x => x.PhoneNumber,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.BatchId, Unspecified.String);
            canView(item, m => m.PartySignatureId, Unspecified.String);
            canView(item, m => m.CheckedBy, Unspecified.String);
            canView(item, m => m.Address, Unspecified.String);
            canView(item, m => m.EmailAddress, Unspecified.String);
            canView(item, m => m.PhoneNumber, Unspecified.String);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, m => m.Code);
            canSelect(item, x => x.BatchId, batches, true);
            canSelect(item, x => x.PartySignatureId, partySignatures, true);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, m => m.Code);
            canSelect(null, m => m.BatchId, batches, true);
            canSelect(null, x => x.PartySignatureId, partySignatures, true);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :BatchCheckedByTests { }
    [TestClass] public class DeletePageTests :BatchCheckedByTests { }
    [TestClass] public class DetailsPageTests :BatchCheckedByTests { }
    [TestClass] public class EditPageTests :BatchCheckedByTests { }
    [TestClass] public class IndexPageTests :BatchCheckedByTests { }
}
