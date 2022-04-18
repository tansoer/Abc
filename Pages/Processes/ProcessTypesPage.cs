using System;
using Abc.Data.Processes;
using Abc.Data.Rules;
using Abc.Domain.Processes;
using Abc.Domain.Rules;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abc.Aids.Reflection;
using Task = System.Threading.Tasks.Task;

namespace Abc.Pages.Processes {
    public sealed class ProcessTypesPage :ViewFactoryPage<ProcessTypesPage, IProcessTypesRepo,
        ProcessType, ProcessTypeView, ProcessTypeData, ProcessTypeViewFactory> {
        protected override string title => ProcessTitles.ProcessTypes;
        internal readonly IThreadTypesRepo threadTypesRepo;
        public ProcessTypesPage(IProcessTypesRepo r, IThreadTypesRepo t) : base(r) {
            threadTypesRepo = t;
            ThreadTypes = new List<ThreadTypeView>();
        }
        private IEnumerable<SelectListItem> sets;
        public IEnumerable<SelectListItem> RuleSets => sets ??= selectListByName<IRuleSetsRepo, IRuleSet, RuleSetData>();
        [BindProperty] public List<ThreadTypeView> ThreadTypes { get; set; }
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void addFields() {
            addField(x => Item.Name);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForViewers() {
            addFieldBefore(field(x => Item.Code), x => Item.Name);
            addFieldBefore(field(x => Item.Details), x => Item.ValidFrom);
            addFieldBefore(field(x => Item.RuleSetId, () => RuleSets, () => RuleSetName(Item.RuleSetId)), x => Item.ValidFrom);
        }
        protected override void fieldsForEditors() => fieldsForViewers();
        public override void LoadDetails() => loadThreadTypes();
        private void loadThreadTypes() => ThreadTypes = loadDetails(threadTypesRepo,
            GetMember.Name<ThreadTypeData>(x => x.ProcessTypeId), ItemId, new ThreadTypeViewFactory().Create);
        protected internal override string pageUrl => ProcessUrls.ProcessTypes;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string RuleSetName(string id) => itemName(RuleSets, id);
        public async Task<IActionResult> OnGetShowThreadTypesAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<ThreadTypeData>(x => x.ProcessTypeId);
            var page = ProcessUrls.ThreadTypes;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }

        public List<ComponentArgs> ThreadTypesInputs => new()
        {
            expandingEditorField(nameof(ThreadTypeView.Name)),
            expandingEditorField(nameof(ThreadTypeView.ValidFrom)),
            expandingEditorField(nameof(ThreadTypeView.ValidTo)),
            expandingEditorField(nameof(ThreadTypeView.Id), true),
        };
        protected override void doAfterOnPostCreate() => createThreadTypes();
        private void createThreadTypes() {
            ThreadTypes.ForEach(x => x.Id = Item.Id);
            foreach (var x in ThreadTypes) threadTypesRepo.Add(viewToTerm(x));
        }
        private ThreadType viewToTerm(ThreadTypeView v) => new ThreadTypeViewFactory().Create(v);
    }
}