using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class ActionApproverViewFactory :AbstractViewFactory<ActionApproverData, ActionApprover, ActionApproverView> {
        protected internal override ActionApprover toObject(ActionApproverData d) => new(d);
    }
}
