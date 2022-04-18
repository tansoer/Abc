using Abc.Aids.Reflection;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Abc.Pages.Processes {
    public sealed class TaskTypesPage :ViewFactoryPage<TaskTypesPage, ITaskTypesRepo,
        TaskType, TaskTypeView, TaskTypeData, TaskTypeViewFactory> {
        protected override string title => ProcessTitles.TaskTypes;
        internal readonly IThreadTypesRepo threadTypesRepo;
        internal readonly IActionTypesRepo actionTypesRepo;
        public TaskTypesPage(ITaskTypesRepo r, IThreadTypesRepo th, IActionTypesRepo at) : base(r) {
            threadTypesRepo = th;
            actionTypesRepo = at;
            ActionTypes = new List<ActionTypeView>();
        }
        private IEnumerable<SelectListItem> threadTypes;
        private IEnumerable<SelectListItem> nextElements;
        private IEnumerable<SelectListItem> previousElements;
        public IEnumerable<SelectListItem> ThreadTypes => threadTypes ??= selectListByName<IThreadTypesRepo, ThreadType, ThreadTypeData>();
        public IEnumerable<SelectListItem> PreviousElements => previousElements ??= selectListByName<ITaskTypesRepo, TaskType, TaskTypeData>();
        public IEnumerable<SelectListItem> NextElements => nextElements ??= selectListByName<ITaskTypesRepo, TaskType, TaskTypeData>();
        [BindProperty] public List<ActionTypeView> ActionTypes { get; set; }
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void addFields() {
            addField(x => Item.Name);
            addField(x => Item.Details);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForViewers() {
            addFieldBefore(field(x => Item.Code), x => Item.Name);
            addFieldBefore(field(x => Item.ThreadTypeId, () => ThreadTypes, () => ThreadTypeName(Item.ThreadTypeId)), x => Item.ValidFrom);
            addFieldBefore(field(x => Item.PreviousElementId, () => PreviousElements, () => PreviousElementName(Item.PreviousElementId)), x => Item.ValidFrom);
            addFieldBefore(field(x => Item.NextElementId, () => NextElements, () => NextElementName(Item.NextElementId)), x => Item.ValidFrom);
        }
        protected override void fieldsForEditors() => fieldsForViewers();
        public override void LoadDetails() => loadActionTypes();
        private void loadActionTypes() => ActionTypes = loadDetails(actionTypesRepo,
            GetMember.Name<ActionTypeData>(x => x.TaskTypeId), ItemId, new ActionTypeViewFactory().Create);

        protected internal override string pageUrl => ProcessUrls.TaskTypes;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string ThreadTypeName(string id) => itemName(ThreadTypes, id);
        public string NextElementName(string id) => itemName(NextElements, id);
        public string PreviousElementName(string id) => itemName(PreviousElements, id);
        public async Task<IActionResult> OnGetShowActionTypesAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<ActionTypeData>(x => x.TaskTypeId);
            var page = ProcessUrls.ActionTypes;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }

        public List<ComponentArgs> ActionTypesInputs => new()
        {
            expandingEditorField(nameof(ActionTypeView.Name)),
            expandingEditorField(nameof(ActionTypeView.ValidFrom)),
            expandingEditorField(nameof(ActionTypeView.ValidTo)),
            expandingEditorField(nameof(ActionTypeView.Id), true),
        };
        protected override void doAfterOnPostCreate() => createActionTypes();
        private void createActionTypes() {
            ActionTypes.ForEach(x => x.Id = Item.Id);
            foreach (var x in ActionTypes) actionTypesRepo.Add(viewToTerm(x));
        }
        private ActionType viewToTerm(ActionTypeView v) => new ActionTypeViewFactory().Create(v);
    }
}