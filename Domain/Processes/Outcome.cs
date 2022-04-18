using Abc.Data.Processes;
using Abc.Domain.Parties.Signatures;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Domain.Processes {
    public sealed class Outcome 
        :ProcessElement<IOutcomesRepo, Outcome, OutcomeData> {
        public Outcome() : this(null) { }
        public Outcome(OutcomeData d) : base(d) { }
        public OutcomeType Type => item<IOutcomeTypesRepo, OutcomeType>(typeId);
        public Action Action => item<IActionsRepo, Action>(actionId);
        internal IReadOnlyList<PartySignature> Approvers => approvers.Select(x => x.Approver).ToList().AsReadOnly();
        internal IReadOnlyList<OutcomeApprover> approvers => list<IOutcomeApproversRepo, OutcomeApprover>(outcomeId, Id);
        public bool IsPossibleOutcome => get(Data?.IsPossibleOutcome);
        internal string typeId => get(Data?.OutcomeTypeId);
        internal string actionId => get(Data?.ActionId);
        internal static string outcomeId => nameOf<OutcomeApproverData>(x => x.OutcomeId);
    }
}
