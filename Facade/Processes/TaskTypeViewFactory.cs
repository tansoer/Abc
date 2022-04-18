using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class TaskTypeViewFactory :AbstractViewFactory<TaskTypeData, TaskType, TaskTypeView> {
        protected internal override TaskType toObject(TaskTypeData d) => new(d);
    }
}
