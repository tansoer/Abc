using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class ActionViewFactory :AbstractViewFactory<ActionData, Action, ActionView> {
        protected internal override Action toObject(ActionData d) => new(d);
    }
}
