using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Action = Abc.Domain.Processes.Action;

namespace Abc.Pages.Processes {
    public sealed class OutcomesPage :ViewFactoryPage<OutcomesPage, IOutcomesRepo,
        Outcome, OutcomeView, OutcomeData, OutcomeViewFactory> {
        protected override string title => ProcessTitles.Outcomes;
        public OutcomesPage(IOutcomesRepo r) :base(r) { }
        private IEnumerable<SelectListItem> types;
        private IEnumerable<SelectListItem> actions;
        public IEnumerable<SelectListItem> OutcomeTypes => types ??= selectListByName<IOutcomeTypesRepo, OutcomeType, OutcomeTypeData>();
        public IEnumerable<SelectListItem> Actions => actions ??= selectListByName<IActionsRepo, Action, ActionData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.OutcomeTypeId);
            tableColumn(x => Item.ActionId);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.OutcomeTypeId, () => OutcomeTypeName(Item.OutcomeTypeId));
            valueViewer(x => Item.ActionId, () => ActionName(Item.ActionId));
            valueViewer(x => Item.IsPossibleOutcome);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.OutcomeTypeId, () => OutcomeTypes);
            valueEditor(x => Item.ActionId, () => Actions);
            valueEditor(x => Item.IsPossibleOutcome);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProcessUrls.Outcomes;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string OutcomeTypeName(string id) => itemName(OutcomeTypes, id);
        public string ActionName(string id) => itemName(Actions, id);
    }
}
