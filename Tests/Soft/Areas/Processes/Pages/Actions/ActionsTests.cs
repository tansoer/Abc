using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Facade.Processes;
using Abc.Infra.Classifiers;
using Abc.Infra.Parties;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Abc.Tests.Soft.Areas.Processes.Pages.Actions {
    public abstract class ActionsTests :BaseProcessTests<ActionView, ActionData> {
        protected List<SelectListItem> actionTypes => createSelectList(db.ActionTypes);
        protected List<SelectListItem> tasks => createSelectList(db.Tasks);
        protected List<SelectListItem> partySignatures => getContext<PartyDb>().PartySignatures
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> actionStatusClassifiers => getContext<ClassifierDb>().Classifiers
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected override string baseUrl() => ProcessUrls.Actions;
        protected override ActionView toView(ActionData d)
            => new ActionViewFactory().Create(new Abc.Domain.Processes.Action(d));
        protected override void changeValuesExceptId(ActionData d) {
            var id = d.Id;
            d = random<ActionData>();
            d.Id = id;
        }
        protected override string getItemId(ActionData d) => d.Id;
        protected override void setItemId(ActionData d, string id) => d.Id = id;
        protected override void addRelatedItems(ActionData d) {
            if (d is null) return;
            addActionType(d.ActionTypeId);
            addTask(d.TaskId);
            addPartySignature(d.InitiatorSignatureId);
            addActionStatusClassifier(d.ActionStatusClassifierId);
        }
        protected override void validateItems(ActionData d1, ActionData d2) {
            allPropertiesAreEqual(d1, d2,
                nameof(d1.RuleContextId),
                nameof(d1.NextElementId),
                nameof(d1.PreviousElementId));
        }
        protected override IEnumerable<Expression<Func<ActionView, object>>> indexPageColumns =>
            new Expression<Func<ActionView, object>>[] {
            x => x.Name,
            x => x.ActionTypeId,
            x => x.TaskId,
            x => x.InitiatorSignatureId,
            x => x.ActionStatusClassifierId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.ActionTypeId, Unspecified.String);
            canView(item, x => x.TaskId, Unspecified.String);
            canView(item, x => x.InitiatorSignatureId, Unspecified.String);
            canView(item, x => x.ActionStatusClassifierId, Unspecified.String);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canSelect(item, x => x.ActionTypeId, actionTypes);
            canSelect(item, x => x.TaskId, tasks);
            canSelect(item, x => x.InitiatorSignatureId, partySignatures);
            canSelect(item, x => x.ActionStatusClassifierId, actionStatusClassifiers);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.ActionTypeId, actionTypes);
            canSelect(null, x => x.TaskId, tasks);
            canSelect(null, x => x.InitiatorSignatureId, partySignatures);
            canSelect(null, x => x.ActionStatusClassifierId, actionStatusClassifiers);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :ActionsTests { }
    [TestClass] public class DeletePageTests :ActionsTests { }
    [TestClass] public class DetailsPageTests :ActionsTests { }
    [TestClass] public class EditPageTests :ActionsTests { }
    [TestClass] public class IndexPageTests :ActionsTests { }
}
