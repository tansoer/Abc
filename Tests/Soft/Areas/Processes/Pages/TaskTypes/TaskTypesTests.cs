using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.TaskTypes {
    public abstract class TaskTypesTests :BaseProcessTests<TaskTypeView, TaskTypeData> {
        protected List<SelectListItem> threadTypes => createSelectList(db.ThreadTypes);
        protected List<SelectListItem> nextElements => createSelectList(db.TaskTypes);
        protected List<SelectListItem> previousElements => createSelectList(db.TaskTypes);
        protected override string baseUrl() => ProcessUrls.TaskTypes;
        protected override TaskTypeView toView(TaskTypeData d)
            => new TaskTypeViewFactory().Create(new Abc.Domain.Processes.TaskType(d));
        protected override void changeValuesExceptId(TaskTypeData d) {
            var id = d.Id;
            d = random<TaskTypeData>();
            d.Id = id;
        }
        protected override string getItemId(TaskTypeData d) => d.Id;
        protected override void setItemId(TaskTypeData d, string id) => d.Id = id;
        protected override void addRelatedItems(TaskTypeData d) {
            if (d is null) return;
            addThreadType(d.ThreadTypeId);
            addTaskType(d.NextElementId);
            addTaskType(d.PreviousElementId);
        }
        protected override void validateItems(TaskTypeData d1, TaskTypeData d2) {
            allPropertiesAreEqual(d1, d2,
                nameof(d1.BaseTypeId),
                nameof(d1.RuleSetId),
                nameof(d1.ConsumerTypeId),
                nameof(d1.ProviderTypeId));
        }
        protected override IEnumerable<Expression<Func<TaskTypeView, object>>> indexPageColumns =>
            new Expression<Func<TaskTypeView, object>>[] {
            x => x.Name,
            x => x.Details,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.ThreadTypeId, Unspecified.String);
            canView(item, x => x.NextElementId, Unspecified.String);
            canView(item, x => x.PreviousElementId, Unspecified.String);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canSelect(item, x => x.ThreadTypeId, threadTypes);
            canSelect(item, x => x.NextElementId, nextElements);
            canSelect(item, x => x.PreviousElementId, previousElements);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.ThreadTypeId, threadTypes);
            canSelect(null, x => x.NextElementId, nextElements);
            canSelect(null, x => x.PreviousElementId, previousElements);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :TaskTypesTests { }
    [TestClass] public class DeletePageTests :TaskTypesTests { }
    [TestClass] public class DetailsPageTests :TaskTypesTests { }
    [TestClass] public class EditPageTests :TaskTypesTests { }
    [TestClass] public class IndexPageTests :TaskTypesTests { }
}
