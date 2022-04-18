using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;

namespace Abc.Domain.Processes {
    public sealed class OutcomeApprover :Entity<OutcomeApproverData> {
        public OutcomeApprover() : this(null) { }
        public OutcomeApprover(OutcomeApproverData d) : base(d) { }
        public PartySignature Approver => item<IPartySignaturesRepo, PartySignature>(ApproverId);
        public Outcome Outcome => item<IOutcomesRepo, Outcome>(OutcomeId);
        public string OutcomeId => get(Data?.OutcomeId);
        public string ApproverId => get(Data?.ApproverSignatureId);
    }
}
