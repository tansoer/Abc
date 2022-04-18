using Abc.Aids.Reflection;
using Abc.Data.Classifiers;
using Abc.Data.Processes;
using Abc.Data.Roles;
using Abc.Data.Rules;
using Abc.Domain.Classifiers;
using Abc.Domain.Processes;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
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
    public sealed class CrmPage :ViewFactoryPage<CrmPage, IProcessesRepo,
        Process, ProcessView, ProcessData, ProcessViewFactory> {
        protected override string title => ProcessTitles.Processes;
        public CrmPage(IProcessesRepo r) :base(r) {}
        private IEnumerable<SelectListItem> contexts;
        private IEnumerable<SelectListItem> types;
        private IEnumerable<SelectListItem> managers;
        private IEnumerable<SelectListItem> initiators;
        private IEnumerable<SelectListItem> classifiers;
        public IEnumerable<SelectListItem> RuleContexts => contexts ??= selectListByName<IRuleContextsRepo, RuleContext, RuleContextData>();
        public IEnumerable<SelectListItem> ProcessTypes => types ??= selectListByName<IProcessTypesRepo,ProcessType, ProcessTypeData>();
        public IEnumerable<SelectListItem> ManagerPartyRoles => managers ??= selectListByName<IPartyRolesRepo, PartyRole, PartyRoleData>();
        public IEnumerable<SelectListItem> InitiatorPartyRoles => initiators ??= selectListByName<IPartyRolesRepo, PartyRole, PartyRoleData>();
        public IEnumerable<SelectListItem> PriorityClassifiers 
            => classifiers ??= selectListByName<IClassifiersRepo, IClassifier, ClassifierData>(x => x.IsTypeOf(ClassifierType.ProcessPriority));
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.RuleContextId, () => RuleContextName(Item.RuleContextId));
            tableColumn(x => Item.ProcessTypeId, () => ProcessTypeName(Item.ProcessTypeId));
            tableColumn(x => Item.ManagerPartyRoleId, () => ManagerPartyRoleName(Item.ManagerPartyRoleId));
            tableColumn(x => Item.InitiatorPartyRoleId, () => InitiatorPartyRoleName(Item.InitiatorPartyRoleId));
            tableColumn(x => Item.PriorityClassifierId, () => PriorityClassifierName(Item.PriorityClassifierId));
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.RuleContextId, () => RuleContextName(Item.RuleContextId));
            valueViewer(x => Item.ProcessTypeId, () => ProcessTypeName(Item.ProcessTypeId));
            valueViewer(x => Item.ManagerPartyRoleId, () => ManagerPartyRoleName(Item.ManagerPartyRoleId));
            valueViewer(x => Item.InitiatorPartyRoleId, () => InitiatorPartyRoleName(Item.InitiatorPartyRoleId));
            valueViewer(x => Item.PriorityClassifierId, () => PriorityClassifierName(Item.PriorityClassifierId));
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.RuleContextId, () => RuleContexts);
            valueEditor(x => Item.ProcessTypeId, () => ProcessTypes);
            valueEditor(x => Item.ManagerPartyRoleId, () => ManagerPartyRoles);
            valueEditor(x => Item.InitiatorPartyRoleId, () => InitiatorPartyRoles);
            valueEditor(x => Item.PriorityClassifierId, () => PriorityClassifiers);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProcessUrls.Processes;     
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string RuleContextName(string id) => itemName(RuleContexts, id);
        public string ProcessTypeName(string id) => itemName(ProcessTypes, id);
        public string ManagerPartyRoleName(string id) => itemName(ManagerPartyRoles, id);
        public string InitiatorPartyRoleName(string id) => itemName(InitiatorPartyRoles, id);
        public string PriorityClassifierName(string id) => itemName(PriorityClassifiers, id);
        public async Task<IActionResult> OnGetShowThreadsAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<ThreadData>(x => x.ProcessId);
            var page = ProcessUrls.Threads;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
    }
}