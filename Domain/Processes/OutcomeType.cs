using Abc.Data.Processes;

namespace Abc.Domain.Processes {
    public sealed class OutcomeType 
        :ProcessElementType<IOutcomeTypesRepo, OutcomeType, OutcomeTypeData> {
        public OutcomeType() : this(null) { }
        public OutcomeType(OutcomeTypeData d) : base(d) { }
        public ActionType ActionType => item<IActionTypesRepo, ActionType>(ActionTypeId);
        public string ActionTypeId => get(Data?.ActionTypeId);
    }
}
