using Abc.Aids.Reflection;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Processes {
    public sealed class ActionTypesPage :ViewFactoryPage<ActionTypesPage, IActionTypesRepo,
        ActionType, ActionTypeView, ActionTypeData, ActionTypeViewFactory> {
        protected override string title => ProcessTitles.ActionTypes;
        internal readonly ITaskTypesRepo taskTypesRepo;
        internal readonly IOutcomeTypesRepo outcomeTypesRepo;
        public ActionTypesPage(IActionTypesRepo r, ITaskTypesRepo t, IOutcomeTypesRepo o) : base(r) {
            taskTypesRepo = t;
            outcomeTypesRepo = o;
            OutcomeTypes = new List<OutcomeTypeView>();
        }
        private IEnumerable<SelectListItem> taskTypes;
        public IEnumerable<SelectListItem> TaskTypes => taskTypes ??= selectListByName<ITaskTypesRepo, TaskType, TaskTypeData>();
        [BindProperty] public List<OutcomeTypeView> OutcomeTypes { get; set; }
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void addFields() {
            addField(x => Item.TaskTypeId, () => TaskTypes, () => TaskTypeName(Item.TaskTypeId));
            addField(x => Item.Name);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForViewers() {
            addFieldBefore(field(x => Item.Code), x => Item.ValidFrom);
            addFieldBefore(field(x => Item.Details), x => Item.ValidFrom);
        }
        protected override void fieldsForEditors() => fieldsForViewers();
        public override void LoadDetails() => loadOutcomeTypes();
        private void loadOutcomeTypes() => OutcomeTypes = loadDetails(outcomeTypesRepo,
            GetMember.Name<OutcomeTypeData>(x => x.ActionTypeId), ItemId, new OutcomeTypeViewFactory().Create);
        protected internal override string pageUrl => ProcessUrls.ActionTypes;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string TaskTypeName(string id) => itemName(TaskTypes, id);

        public List<ComponentArgs> OutcomeTypesInputs => new()
        {
            expandingEditorField(nameof(OutcomeTypeView.Name)),
            expandingEditorField(nameof(OutcomeTypeView.ValidFrom)),
            expandingEditorField(nameof(OutcomeTypeView.ValidTo)),
            expandingEditorField(nameof(OutcomeTypeView.Id), true),
        };
        protected override void doAfterOnPostCreate() => createOutcomeTypes();
        private void createOutcomeTypes() {
            OutcomeTypes.ForEach(x => x.Id = Item.Id);
            foreach (var x in OutcomeTypes) outcomeTypesRepo.Add(viewToTerm(x));
        }
        private OutcomeType viewToTerm(OutcomeTypeView v) => new OutcomeTypeViewFactory().Create(v);
    }
}
