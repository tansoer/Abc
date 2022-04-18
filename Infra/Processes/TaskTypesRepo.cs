using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class TaskTypesRepo :EntityRepo<TaskType, TaskTypeData>, ITaskTypesRepo {
        public TaskTypesRepo(ProcessDb c = null) : base(c, c?.TaskTypes) { }
    }
}
