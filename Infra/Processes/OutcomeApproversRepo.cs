using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class OutcomeApproversRepo :EntityRepo<OutcomeApprover, OutcomeApproverData>, IOutcomeApproversRepo {
        public OutcomeApproversRepo(ProcessDb c = null) : base(c, c?.OutcomeApprovers) { }
    }
}

