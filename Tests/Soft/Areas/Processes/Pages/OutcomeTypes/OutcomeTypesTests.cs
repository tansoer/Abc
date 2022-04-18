using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.OutcomeTypes {
    public abstract class OutcomeTypesTests :BaseProcessTests<OutcomeTypeView, OutcomeTypeData> {
        protected List<SelectListItem> actionTypes => createSelectList(db.ActionTypes);
        protected override string baseUrl() => ProcessUrls.OutcomeTypes;
        protected override OutcomeTypeView toView(OutcomeTypeData d)
            => new OutcomeTypeViewFactory().Create(new Abc.Domain.Processes.OutcomeType(d));
        protected override void changeValuesExceptId(OutcomeTypeData d) {
            var id = d.Id;
            d = random<OutcomeTypeData>();
            d.Id = id;
        }
        protected override string getItemId(OutcomeTypeData d) => d.Id;
        protected override void setItemId(OutcomeTypeData d, string id) => d.Id = id;
        protected override void addRelatedItems(OutcomeTypeData d) {
            if (d is null) return;
            addActionType(d.ActionTypeId);
        }
        protected override void validateItems(OutcomeTypeData d1, OutcomeTypeData d2) {
            allPropertiesAreEqual(d1, d2,
                nameof(d1.RuleSetId),
                nameof(d1.BaseTypeId),
                nameof(d1.NextElementId),
                nameof(d1.PreviousElementId));
        }
        protected override IEnumerable<Expression<Func<OutcomeTypeView, object>>> indexPageColumns =>
            new Expression<Func<OutcomeTypeView, object>>[] {
            x => x.Name,
            x => x.ActionTypeId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.ActionTypeId, Unspecified.String);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canSelect(item, x => x.ActionTypeId, actionTypes);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.ActionTypeId, actionTypes);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :OutcomeTypesTests { }
    [TestClass] public class DeletePageTests :OutcomeTypesTests { }
    [TestClass] public class DetailsPageTests :OutcomeTypesTests { }
    [TestClass] public class EditPageTests :OutcomeTypesTests { }
    [TestClass] public class IndexPageTests :OutcomeTypesTests { }
}
