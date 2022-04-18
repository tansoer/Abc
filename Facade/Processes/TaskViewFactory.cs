using Abc.Aids.Methods;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class TaskViewFactory :AbstractViewFactory<TaskData, ITask, TaskView> {
        protected internal override ITask toObject(TaskData d) => TaskFactory.Create(d);
        public override TaskView Create(ITask o) {
            var v = new TaskView();
            if (o is not null) Copy.Members(o.Data, v);
            v.Name = o?.GetName();
            return v;
        }
    }
}
