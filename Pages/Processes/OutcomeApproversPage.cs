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
    public sealed class OutcomeApproversPage :ViewFactoryPage<OutcomeApproversPage, IOutcomeApproversRepo,
        OutcomeApprover, OutcomeApproverView, OutcomeApproverData, OutcomeApproverViewFactory> {
        protected override string title => ProcessTitles.OutcomeApprovers;
        public OutcomeApproversPage(IOutcomeApproversRepo r) :base(r) { }
        private IEnumerable<SelectListItem> outcomes;
        private IEnumerable<SelectListItem> signatures;
        public IEnumerable<SelectListItem> Outcomes => outcomes ??= selectListByName<IOutcomesRepo, Outcome, OutcomeData>();
        public IEnumerable<SelectListItem> ApproverSignatures => signatures ??= selectListByName<IPartySignaturesRepo, PartySignature, PartySignatureData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.OutcomeId);
            tableColumn(x => Item.ApproverSignatureId);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.OutcomeId, () => OutcomeName(Item.OutcomeId));
            valueViewer(x => Item.ApproverSignatureId, () => ApproverSignatureName(Item.ApproverSignatureId));
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.OutcomeId, () => Outcomes);
            valueEditor(x => Item.ApproverSignatureId, () => ApproverSignatures);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProcessUrls.OutcomeApprovers;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string OutcomeName(string id) => itemName(Outcomes, id);
        public string ApproverSignatureName(string id) => itemName(ApproverSignatures, id);
    }
}