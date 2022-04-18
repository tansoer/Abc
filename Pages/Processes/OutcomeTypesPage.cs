using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Processes {
    public sealed class OutcomeTypesPage :ViewFactoryPage<OutcomeTypesPage, IOutcomeTypesRepo,
        OutcomeType, OutcomeTypeView, OutcomeTypeData, OutcomeTypeViewFactory> {
        protected override string title => ProcessTitles.OutcomeTypes;
        public OutcomeTypesPage(IOutcomeTypesRepo r) :base(r) {}
        private IEnumerable<SelectListItem> actionTypes;
        public IEnumerable<SelectListItem> ActionTypes => actionTypes ??= selectListByName<IActionTypesRepo, ActionType, ActionTypeData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void addFields() {
            addField(x => Item.ActionTypeId, () => ActionTypes, () => ActionTypeName(Item.ActionTypeId));
            addField(x => Item.Name);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForViewers() {
            addFieldBefore(field(x => Item.Code), x => Item.Name);
            addFieldBefore(field(x => Item.Details), x => Item.ValidFrom);
        }
        protected override void fieldsForEditors() => fieldsForViewers();
        protected internal override string pageUrl => ProcessUrls.OutcomeTypes;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string ActionTypeName(string id) => itemName(ActionTypes, id);
    }
}
