using Abc.Data.Processes;
using Abc.Data.Roles;
using Abc.Domain.Processes;
using Abc.Domain.Roles;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Processes {
    public sealed class TaskParticipantsPage :ViewFactoryPage<TaskParticipantsPage, ITaskParticipantsRepo,
        TaskParticipant, TaskParticipantView, TaskParticipantData, TaskParticipantViewFactory> {
        protected override string title => ProcessTitles.TaskParticipants;
        public TaskParticipantsPage(ITaskParticipantsRepo r) :base(r) {}
        private IEnumerable<SelectListItem> roles;
        private IEnumerable<SelectListItem> tasks;
        public IEnumerable<SelectListItem> PartyRoles => roles ??= selectListByName<IPartyRolesRepo, PartyRole, PartyRoleData>();
        public IEnumerable<SelectListItem> Tasks => tasks ??= selectListByName<ITasksRepo, ITask, TaskData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.PartyRoleId);
            tableColumn(x => Item.TaskId);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.PartyRoleId, () => PartyRoleName(Item.PartyRoleId));
            valueViewer(x => Item.TaskId, () => TaskName(Item.TaskId));
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.PartyRoleId, () => PartyRoles);
            valueEditor(x => Item.TaskId, () => Tasks);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProcessUrls.TaskParticipants;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string PartyRoleName(string id) => itemName(PartyRoles, id);
        public string TaskName(string id) => itemName(Tasks, id);
    }
}
