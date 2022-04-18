using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.ActionTypes {
    public abstract class ActionTypesTests :BaseProcessTests<ActionTypeView, ActionTypeData> {
        protected List<SelectListItem> tasks => createSelectList(db.Tasks);
        protected override string baseUrl() => ProcessUrls.ActionTypes;
        protected override ActionTypeView toView(ActionTypeData d)
            => new ActionTypeViewFactory().Create(new Abc.Domain.Processes.ActionType(d));
        protected override void changeValuesExceptId(ActionTypeData d) {
            var id = d.Id;
            d = random<ActionTypeData>();
            d.Id = id;
        }
        protected override string getItemId(ActionTypeData d) => d.Id;
        protected override void setItemId(ActionTypeData d, string id) => d.Id = id;
        protected override void addRelatedItems(ActionTypeData d) {
            if (d is null) return;
            addTaskType(d.TaskTypeId);
        }
        protected override void validateItems(ActionTypeData d1, ActionTypeData d2) {
            allPropertiesAreEqual(d1, d2,
                nameof(d1.RuleSetId),
                nameof(d1.BaseTypeId),
                nameof(d1.NextElementId),
                nameof(d1.PreviousElementId));
        }
        protected override IEnumerable<Expression<Func<ActionTypeView, object>>> indexPageColumns =>
            new Expression<Func<ActionTypeView, object>>[] {
            x => x.TaskTypeId,
            x => x.Name,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.TaskTypeId, Unspecified.String);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canSelect(item, x => x.TaskTypeId, tasks);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.TaskTypeId, tasks);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :ActionTypesTests { }
    [TestClass] public class DeletePageTests :ActionTypesTests { }
    [TestClass] public class DetailsPageTests :ActionTypesTests { }
    [TestClass] public class EditPageTests :ActionTypesTests { }
    [TestClass] public class IndexPageTests :ActionTypesTests { }
}
