using Abc.Data.Processes;

namespace Abc.Domain.Processes {
    public sealed class TaskRouting : TaskBase {
        public TaskRouting() : this(null) { }
        public TaskRouting(TaskData d) : base(d) { }
        public bool IsEscalating { get; set; }
    }
}
