using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class ProcessTypeViewFactory :AbstractViewFactory<ProcessTypeData, ProcessType, ProcessTypeView> {
        protected internal override ProcessType toObject(ProcessTypeData d) => new(d);
    }
}
