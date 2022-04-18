using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Data.Rules;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Processes;
using Abc.Domain.Rules;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Abc.Pages.Processes {
    public sealed class TasksPage :ViewPage<TasksPage, ITasksRepo,
        ITask, TaskView, TaskData> {
        protected override string title => ProcessTitles.Tasks;
        public TasksPage(ITasksRepo r) :base(r) {}
        private IEnumerable<SelectListItem> threads;
        private IEnumerable<SelectListItem> contexts;
        private IEnumerable<SelectListItem> nextElements;
        private IEnumerable<SelectListItem> previousElements;
        private IEnumerable<SelectListItem> fromPartyAddresses;
        private IEnumerable<SelectListItem> toPartyAddresses;
        public IEnumerable<SelectListItem> Threads => threads ??= selectListByName<IThreadsRepo, Thread, ThreadData>();
        public IEnumerable<SelectListItem> RuleContexts => contexts ??= selectListByName<IRuleContextsRepo, RuleContext, RuleContextData>();
        public IEnumerable<SelectListItem> NextElements => nextElements ??= selectListByName<ITasksRepo, ITask, TaskData>();
        public IEnumerable<SelectListItem> PreviousElements => previousElements ??= selectListByName<ITasksRepo, ITask, TaskData>();
        public IEnumerable<SelectListItem> FromPartyAddresses => fromPartyAddresses ??= selectListByName<IPartyContactsRepo, IPartyContact, PartyContactData>();
        public IEnumerable<SelectListItem> ToPartyAddresses => toPartyAddresses ??= selectListByName<IPartyContactsRepo, IPartyContact, PartyContactData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.ThreadId);
            tableColumn(x => Item.RuleContextId);
            tableColumn(x => Item.NextElementId);
            tableColumn(x => Item.PreviousElementId);
            tableColumn(x => Item.FromPartyAddressId);
            tableColumn(x => Item.ToPartyAddressId);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ThreadId, () => ThreadName(Item.ThreadId));
            valueViewer(x => Item.RuleContextId, () => RuleContextName(Item.RuleContextId));
            valueViewer(x => Item.NextElementId, () => NextElementName(Item.NextElementId));
            valueViewer(x => Item.PreviousElementId, () => PreviousElementName(Item.PreviousElementId));
            valueViewer(x => Item.FromPartyAddressId, () => FromPartyAddressName(Item.FromPartyAddressId));
            valueViewer(x => Item.ToPartyAddressId, () => ToPartyAddressName(Item.ToPartyAddressId));
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ThreadId, () => Threads);
            valueEditor(x => Item.RuleContextId, () => RuleContexts);
            valueEditor(x => Item.NextElementId, () => NextElements);
            valueEditor(x => Item.PreviousElementId, () => PreviousElements);
            valueEditor(x => Item.FromPartyAddressId, () => FromPartyAddresses);
            valueEditor(x => Item.ToPartyAddressId, () => ToPartyAddresses);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProcessUrls.Tasks;
        protected internal override ITask toObject(TaskView view) => new TaskViewFactory().Create(view);
        protected internal override TaskView toView(ITask obj) => new TaskViewFactory().Create(obj);
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string ThreadName(string id) => itemName(Threads, id);
        public string RuleContextName(string id) => itemName(RuleContexts, id);
        public string NextElementName(string id) => itemName(NextElements, id);
        public string PreviousElementName(string id) => itemName(PreviousElements, id);
        public string FromPartyAddressName(string id) => itemName(FromPartyAddresses, id);
        public string ToPartyAddressName(string id) => itemName(ToPartyAddresses, id);
    }
}
