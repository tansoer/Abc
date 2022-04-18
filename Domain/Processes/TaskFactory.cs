using Abc.Data.Processes;

namespace Abc.Domain.Processes {
    public static class TaskFactory {

        public static ITask Create(TaskData d) 
            => (d?.IsEscalation ?? false)? new TaskRouting(d): new Task(d);
    }
}
