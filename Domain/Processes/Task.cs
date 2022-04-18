using Abc.Data.Processes;

namespace Abc.Domain.Processes {
    public sealed class Task :TaskBase {
        public Task() : this(null) { }
        public Task(TaskData d) : base(d) { }
    }
}
