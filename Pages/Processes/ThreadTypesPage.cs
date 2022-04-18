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
    public sealed class ThreadTypesPage :ViewFactoryPage<ThreadTypesPage, IThreadTypesRepo,
        ThreadType, ThreadTypeView, ThreadTypeData, ThreadTypeViewFactory> {
        protected override string title => ProcessTitles.ThreadTypes;
        internal readonly IProcessTypesRepo processTypesRepo;
        internal readonly ITaskTypesRepo taskTypesRepo;
        public ThreadTypesPage(IThreadTypesRepo r, IProcessTypesRepo p, ITaskTypesRepo t) : base(r) {
            processTypesRepo = p;
            taskTypesRepo = t;
            TaskTypes = new List<TaskTypeView>();
        }
        private IEnumerable<SelectListItem> processTypes;
        public IEnumerable<SelectListItem> ProcessTypes => processTypes ??= selectListByName<IProcessTypesRepo, ProcessType, ProcessTypeData>();
        [BindProperty] public List<TaskTypeView> TaskTypes { get; set; }
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void addFields() {
            addField(x => Item.Name);
            addField(x => Item.ProcessTypeId, () => ProcessTypes, () => ProcessTypeName(Item.ProcessTypeId));
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForViewers() {
            addFieldBefore(field(x => Item.Code), x => Item.Name);
            addFieldBefore(field(x => Item.Details), x => Item.ValidFrom);
        }
        protected override void fieldsForEditors() => fieldsForViewers();
        public override void LoadDetails() => loadTaskTypes();
        private void loadTaskTypes() => TaskTypes = loadDetails(taskTypesRepo,
            GetMember.Name<TaskTypeData>(x => x.ThreadTypeId), ItemId, new TaskTypeViewFactory().Create);
        protected internal override string pageUrl => ProcessUrls.ThreadTypes;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string ProcessTypeName(string id) => itemName(ProcessTypes, id);
        public async Task<IActionResult> OnGetShowTaskTypesAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<TaskTypeData>(x => x.ThreadTypeId);
            var page = ProcessUrls.TaskTypes;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }

        public List<ComponentArgs> TaskTypesInputs => new()
        {
            expandingEditorField(nameof(TaskTypeView.Name)),
            expandingEditorField(nameof(TaskTypeView.ValidFrom)),
            expandingEditorField(nameof(TaskTypeView.ValidTo)),
            expandingEditorField(nameof(TaskTypeView.Id), true),
        };
        protected override void doAfterOnPostCreate() => createTaskTypes();
        private void createTaskTypes() {
            TaskTypes.ForEach(x => x.Id = Item.Id);
            foreach (var x in TaskTypes) taskTypesRepo.Add(viewToTerm(x));
        }
        private TaskType viewToTerm(TaskTypeView v) => new TaskTypeViewFactory().Create(v);
    }
}