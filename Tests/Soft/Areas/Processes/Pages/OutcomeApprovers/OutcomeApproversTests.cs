using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Facade.Processes;
using Abc.Infra.Parties;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.OutcomeApprovers {
    public abstract class OutcomeApproversTests :BaseProcessTests<OutcomeApproverView, OutcomeApproverData> {
        protected List<SelectListItem> outcomes => createSelectList(db.Outcomes);
        protected List<SelectListItem> approverSignatures => getContext<PartyDb>().PartySignatures
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected override string baseUrl() => ProcessUrls.OutcomeApprovers;
        protected override OutcomeApproverView toView(OutcomeApproverData d)
            => new OutcomeApproverViewFactory().Create(new Abc.Domain.Processes.OutcomeApprover(d));
        protected override void changeValuesExceptId(OutcomeApproverData d) {
            var id = d.Id;
            d = random<OutcomeApproverData>();
            d.Id = id;
        }
        protected override string getItemId(OutcomeApproverData d) => d.Id;
        protected override void setItemId(OutcomeApproverData d, string id) => d.Id = id;
        protected override void addRelatedItems(OutcomeApproverData d) {
            if (d is null) return;
            addOutcome(d.OutcomeId);
            addPartySignature(d.ApproverSignatureId);
        }
        protected override IEnumerable<Expression<Func<OutcomeApproverView, object>>> indexPageColumns =>
            new Expression<Func<OutcomeApproverView, object>>[] {
            x => x.Name,
            x => x.OutcomeId,
            x => x.ApproverSignatureId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.OutcomeId, Unspecified.String);
            canView(item, x => x.ApproverSignatureId, Unspecified.String);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canSelect(item, x => x.OutcomeId, outcomes);
            canSelect(item, x => x.ApproverSignatureId, approverSignatures);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.OutcomeId, outcomes);
            canSelect(null, x => x.ApproverSignatureId, approverSignatures);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :OutcomeApproversTests { }
    [TestClass] public class DeletePageTests :OutcomeApproversTests { }
    [TestClass] public class DetailsPageTests :OutcomeApproversTests { }
    [TestClass] public class EditPageTests :OutcomeApproversTests { }
    [TestClass] public class IndexPageTests :OutcomeApproversTests { }
}
