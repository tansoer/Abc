using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class OutcomeTypesRepo :EntityRepo<OutcomeType, OutcomeTypeData>, IOutcomeTypesRepo {
        public OutcomeTypesRepo(ProcessDb c = null) : base(c, c?.OutcomeTypes) { }
    }
}