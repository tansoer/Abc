using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.ThreadTypes {
    public abstract class ThreadTypesTests :BaseProcessTests<ThreadTypeView, ThreadTypeData> {
        protected List<SelectListItem> processTypes => createSelectList(db.ProcessTypes);
        protected override string baseUrl() => ProcessUrls.ThreadTypes;
        protected override ThreadTypeView toView(ThreadTypeData d)
            => new ThreadTypeViewFactory().Create(new Abc.Domain.Processes.ThreadType(d));
        protected override void changeValuesExceptId(ThreadTypeData d) {
            var id = d.Id;
            d = random<ThreadTypeData>();
            d.Id = id;
        }
        protected override string getItemId(ThreadTypeData d) => d.Id;
        protected override void setItemId(ThreadTypeData d, string id) => d.Id = id;
        protected override void addRelatedItems(ThreadTypeData d) {
            if (d is null) return;
            addProcessType(d.ProcessTypeId);
        }
        protected override void validateItems(ThreadTypeData d1, ThreadTypeData d2) {
            allPropertiesAreEqual(d1, d2,
                nameof(d1.RuleSetId),
                nameof(d1.BaseTypeId),
                nameof(d1.NextElementId),
                nameof(d1.PreviousElementId));
        }
        protected override IEnumerable<Expression<Func<ThreadTypeView, object>>> indexPageColumns =>
            new Expression<Func<ThreadTypeView, object>>[] {
            x => x.Name,
            x => x.ProcessTypeId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.ProcessTypeId, Unspecified.String);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canSelect(item, x => x.ProcessTypeId, processTypes);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.ProcessTypeId, processTypes);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :ThreadTypesTests { }
    [TestClass] public class DeletePageTests :ThreadTypesTests { }
    [TestClass] public class DetailsPageTests :ThreadTypesTests { }
    [TestClass] public class EditPageTests :ThreadTypesTests { }
    [TestClass] public class IndexPageTests :ThreadTypesTests { }
}
