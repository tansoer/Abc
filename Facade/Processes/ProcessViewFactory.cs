using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class ProcessViewFactory :AbstractViewFactory<ProcessData, Process, ProcessView> {
        protected internal override Process toObject(ProcessData d) => new(d);
    }
}
