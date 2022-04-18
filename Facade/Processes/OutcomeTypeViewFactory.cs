using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class OutcomeTypeViewFactory :AbstractViewFactory<OutcomeTypeData, OutcomeType, OutcomeTypeView> {
        protected internal override OutcomeType toObject(OutcomeTypeData d) => new(d);
    }
}
