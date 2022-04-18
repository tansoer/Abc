using Abc.Data.Processes;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Processes;
using Abc.Domain.Parties.Signatures;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Domain.Processes {
    public sealed class Action :ProcessElement<IActionsRepo, Action, ActionData> {
        public Action() : this(null) { }
        public Action(ActionData d) : base(d) { }

        public ActionType Type => item<IActionTypesRepo, ActionType>(ActionTypeId);
        public string ActionTypeId => get(Data?.ActionTypeId);

        public ActionStatus ActionStatus => item<IClassifiersRepo, IClassifier>(ActionStatusClassifierId) as ActionStatus;
        public string ActionStatusClassifierId => get(Data?.ActionStatusClassifierId);
        
        public ITask Task => item<ITasksRepo, ITask>(TaskId);
        public string TaskId => get(Data?.TaskId);

        public PartySignature InitiatorSignature => item<IPartySignaturesRepo, PartySignature>(InitiatorSignatureId);
        public string InitiatorSignatureId => get(Data?.InitiatorSignatureId);

        public IReadOnlyList<PartySignature> Approvers => ActionApprovers.Select(x => x.Approver).ToList().AsReadOnly();
        public IReadOnlyList<ActionApprover> ActionApprovers => list<IActionApproversRepo, ActionApprover>(actionId, Id);

        public IReadOnlyList<Outcome> PossibleOutcomes => Outcomes.Where(x => x.IsPossibleOutcome).ToList().AsReadOnly();
        public IReadOnlyList<Outcome> ActualOutcomes => Outcomes.Where(x => !x.IsPossibleOutcome).ToList().AsReadOnly();
        public IReadOnlyList<Outcome> Outcomes => list<IOutcomesRepo, Outcome>(actionId, Id);
        
        internal static string actionId => nameOf<ActionApproverData>(x => x.ActionId);
    }
}
