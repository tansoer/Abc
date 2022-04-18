using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class ThreadTypeViewFactory :AbstractViewFactory<ThreadTypeData, ThreadType, ThreadTypeView> {
        protected internal override ThreadType toObject(ThreadTypeData d) => new(d);
    }
}
