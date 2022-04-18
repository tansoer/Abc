using Abc.Data.Processes;

namespace Abc.Domain.Processes {
    public sealed class ThreadType :ProcessElementType<IThreadTypesRepo, ThreadType, ThreadTypeData > {
        public ThreadType() : this(null) { }
        public ThreadType(ThreadTypeData d) : base(d) { }
        public ProcessType Process => item<IProcessTypesRepo, ProcessType>(processId);
        public override ThreadType Previous => base.Previous;
        public override ThreadType Next => base.Next;
        internal string processId { get; set; }
    }
}
