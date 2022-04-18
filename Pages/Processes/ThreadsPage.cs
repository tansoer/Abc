using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Processes {
    public sealed class ThreadsPage :ViewFactoryPage<ThreadsPage, IThreadsRepo,
        Thread, ThreadView, ThreadData, ThreadViewFactory> {
        protected override string title => ProcessTitles.Threads;
        public ThreadsPage(IThreadsRepo r) : base(r) {}
        private IEnumerable<SelectListItem> types;
        private IEnumerable<SelectListItem> processes;
        private IEnumerable<SelectListItem> signatures;
        public IEnumerable<SelectListItem> ThreadTypes => types ??= selectListByName<IThreadTypesRepo, ThreadType, ThreadTypeData>();
        public IEnumerable<SelectListItem> Processes => processes ??= selectListByName<IProcessesRepo, Process, ProcessData>();
        public IEnumerable<SelectListItem> TerminatorSignatures => signatures ??= selectListByName<IPartySignaturesRepo, PartySignature, PartySignatureData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.ThreadTypeId, () => ThreadTypeName(Item.ThreadTypeId));
            tableColumn(x => Item.ProcessId, () => ProcessName(Item.ProcessId));
            tableColumn(x => Item.TerminatorSignatureId, () => TerminatorSignatureName(Item.TerminatorSignatureId));
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ThreadTypeId, () => ThreadTypeName(Item.ThreadTypeId));
            valueViewer(x => Item.ProcessId, () => ProcessName(Item.ProcessId));
            valueViewer(x => Item.TerminatorSignatureId, () => TerminatorSignatureName(Item.TerminatorSignatureId));
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ThreadTypeId, () => ThreadTypes);
            valueEditor(x => Item.ProcessId, () => Processes);
            valueEditor(x => Item.TerminatorSignatureId, () => TerminatorSignatures);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProcessUrls.Threads;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string ThreadTypeName(string id) => itemName(ThreadTypes, id);
        public string ProcessName(string id) => itemName(Processes, id);
        public string TerminatorSignatureName(string id) => itemName(TerminatorSignatures, id);
    }
}