using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class ThreadViewFactory :AbstractViewFactory<ThreadData, Thread, ThreadView> {
        protected internal override Thread toObject(ThreadData d) => new(d);
    }
}
