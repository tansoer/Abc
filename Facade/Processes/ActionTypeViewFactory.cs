using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class ActionTypeViewFactory :AbstractViewFactory<ActionTypeData, ActionType, ActionTypeView> {
        protected internal override ActionType toObject(ActionTypeData d) => new(d);
    }
}
