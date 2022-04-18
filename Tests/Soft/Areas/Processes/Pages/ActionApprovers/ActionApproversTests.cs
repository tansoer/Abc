using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Infra.Parties;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.ActionApprovers {
    public abstract class ActionApproversTests :BaseProcessTests<ActionApproverView, ActionApproverData> {
        protected List<SelectListItem> actions => createSelectList(db.Actions);
        protected List<SelectListItem> approverSignatures => getContext<PartyDb>().PartySignatures
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected override ActionApproverView toView(ActionApproverData d)
            => new ActionApproverViewFactory().Create(new ActionApprover(d));
        protected override string baseUrl() => ProcessUrls.ActionApprovers;
        protected override void changeValuesExceptId(ActionApproverData d) {
            var id = d.Id;
            d = random<ActionApproverData>();
            d.Id = id;
        }
        protected override string getItemId(ActionApproverData d) => d.Id;
        protected override void setItemId(ActionApproverData d, string id) => d.Id = id;
        protected override void addRelatedItems(ActionApproverData d) {
            if (d is null) return;
            addAction(d.ActionId);
            addPartySignature(d.ApproverSignatureId);
        }
        protected override IEnumerable<Expression<Func<ActionApproverView, object>>> indexPageColumns =>
            new Expression<Func<ActionApproverView, object>>[] {
            x => x.Name,
            x => x.ActionId,
            x => x.ApproverSignatureId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.ActionId, Unspecified.String);
            canView(item, x => x.ApproverSignatureId, Unspecified.String);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canSelect(item, x => x.ActionId, actions);
            canSelect(item, x => x.ApproverSignatureId, approverSignatures);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.ActionId, actions);
            canSelect(null, x => x.ApproverSignatureId, approverSignatures);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :ActionApproversTests { }
    [TestClass] public class DeletePageTests :ActionApproversTests { }
    [TestClass] public class DetailsPageTests :ActionApproversTests { }
    [TestClass] public class EditPageTests :ActionApproversTests { }
    [TestClass] public class IndexPageTests :ActionApproversTests { }
}
