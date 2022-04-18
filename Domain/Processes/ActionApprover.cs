using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;

namespace Abc.Domain.Processes {
    public sealed class ActionApprover :Entity<ActionApproverData> {
        public ActionApprover() : this(null) { }
        public ActionApprover(ActionApproverData d) : base(d) { }
        public PartySignature Approver => item<IPartySignaturesRepo, PartySignature>(ApproverSignatureId);
        public Action Action => item<IActionsRepo, Action>(ActionId);
        public string ActionId => get(Data?.ActionId);
        public string ApproverSignatureId => get(Data?.ApproverSignatureId);
    }
}
