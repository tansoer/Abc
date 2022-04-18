using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Action = Abc.Domain.Processes.Action;

namespace Abc.Pages.Processes {
    public sealed class ActionApproversPage :ViewFactoryPage<ActionApproversPage, IActionApproversRepo,
        ActionApprover, ActionApproverView, ActionApproverData, ActionApproverViewFactory> {
        protected override string title => ProcessTitles.ActionApprovers;
        public ActionApproversPage(IActionApproversRepo r) :base(r) {}
        private IEnumerable<SelectListItem> actions;
        private IEnumerable<SelectListItem> signatures;
        public IEnumerable<SelectListItem> Actions => actions ??= selectListByName<IActionsRepo, Action, ActionData>();
        public IEnumerable<SelectListItem> ApproverSignatures => signatures ??= selectListByName<IPartySignaturesRepo, PartySignature, PartySignatureData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.ActionId);
            tableColumn(x => Item.ApproverSignatureId);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ActionId, () => ActionName(Item.ActionId));
            valueViewer(x => Item.ApproverSignatureId, () => ApproverSignatureName(Item.ApproverSignatureId));
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ActionId, () => Actions);
            valueEditor(x => Item.ApproverSignatureId, () => ApproverSignatures);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProcessUrls.ActionApprovers;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string ActionName(string id) => itemName(Actions, id);
        public string ApproverSignatureName(string id) => itemName(ApproverSignatures, id);
    }
}
