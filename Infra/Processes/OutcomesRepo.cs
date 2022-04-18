using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
namespace Abc.Infra.Processes
{
    public sealed class OutcomesRepo :EntityRepo<Outcome, OutcomeData>, IOutcomesRepo {
        public OutcomesRepo(ProcessDb c = null) : base(c, c?.Outcomes) { }
    }
}