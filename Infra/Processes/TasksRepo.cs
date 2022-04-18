using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class TasksRepo :PagedRepo<ITask, TaskData>, ITasksRepo {
        public TasksRepo(ProcessDb c = null) : base(c, c?.Tasks) { }
        protected internal override ITask toDomainObject(TaskData d) => TaskFactory.Create(d);
    }
}
