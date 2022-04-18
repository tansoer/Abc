using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.Outcomes {
    public abstract class OutcomesTests :BaseProcessTests<OutcomeView, OutcomeData> {
        protected List<SelectListItem> outcomeTypes => createSelectList(db.OutcomeTypes);
        protected List<SelectListItem> actions => createSelectList(db.Actions);
        protected override string baseUrl() => ProcessUrls.Outcomes;
        protected override void changeValuesExceptId(OutcomeData d) {
            var id = d.Id;
            d = random<OutcomeData>();
            d.Id = id;
        }
        protected override string getItemId(OutcomeData d) => d.Id;
        protected override void setItemId(OutcomeData d, string id) => d.Id = id;
        protected override void addRelatedItems(OutcomeData d) {
            if (d is null) return;
            addOutcomeType(d.OutcomeTypeId);
            addAction(d.ActionId);
        }
        protected override void validateItems(OutcomeData d1, OutcomeData d2) {
            allPropertiesAreEqual(d1, d2,
                nameof(d1.RuleContextId), 
                nameof(d1.NextElementId),
                nameof(d1.PreviousElementId));
        }
        protected override OutcomeView toView(OutcomeData d) => new OutcomeViewFactory().Create(new Outcome(d));
        protected override IEnumerable<Expression<Func<OutcomeView, object>>> indexPageColumns =>
            new Expression<Func<OutcomeView, object>>[] {
            x => x.Name,
            x => x.OutcomeTypeId,
            x => x.ActionId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            OutcomeView v = toView(item);
            canView(v, x => x.Name);
            canView(v, x => x.Code);
            canView(v, x => x.Details);
            canView(v, x => x.OutcomeTypeId, Unspecified.String);
            canView(v, x => x.ActionId, Unspecified.String);
            canViewCheckBox(v, x => x.IsPossibleOutcome);
            canView(v, x => x.ValidFrom);
            canView(v, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            OutcomeView v = toView(item);
            canEdit(v, x => x.Name, true);
            canEdit(v, x => x.Code);
            canEdit(v, x => x.Details);
            canSelect(v, x => x.OutcomeTypeId, outcomeTypes);
            canSelect(v, x => x.ActionId, actions);
            canClickCheckBox(v, x => x.IsPossibleOutcome, true, true);
            canEdit(v, x => x.ValidFrom);
            canEdit(v, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.OutcomeTypeId, outcomeTypes);
            canSelect(null, x => x.ActionId, actions);
            canClickCheckBox(null, x => x.IsPossibleOutcome, true, true);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :OutcomesTests { }
    [TestClass] public class DeletePageTests :OutcomesTests { }
    [TestClass] public class DetailsPageTests :OutcomesTests { }
    [TestClass] public class EditPageTests :OutcomesTests { }
    [TestClass] public class IndexPageTests :OutcomesTests { }
}
