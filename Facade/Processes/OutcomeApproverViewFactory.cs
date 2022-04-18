using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class OutcomeApproverViewFactory :AbstractViewFactory<OutcomeApproverData, OutcomeApprover, OutcomeApproverView> {
        protected internal override OutcomeApprover toObject(OutcomeApproverData d) => new(d);
    }
}