using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class OutcomeViewFactory :AbstractViewFactory<OutcomeData, Outcome, OutcomeView> {
        protected internal override Outcome toObject(OutcomeData d) => new(d);
    }
}
