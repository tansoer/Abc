using Abc.Data.Processes;
using Abc.Domain.Parties.Signatures;
using System.Collections.Generic;

namespace Abc.Domain.Processes {
    public sealed class Thread :ProcessElement<IThreadsRepo, Thread, ThreadData> {
        public Thread() : this(null) { }
        public Thread(ThreadData d) : base(d) { }
        public ThreadType Type => item<IThreadTypesRepo, ThreadType>(typeId);
        public PartySignature Terminator => item<IPartySignaturesRepo, PartySignature>(terminatorId);
        public Process Process => item<IProcessesRepo, Process>(processId);
        public IReadOnlyList<ITask> Tasks => list<ITasksRepo, ITask>(threadId, Id);
        public override Thread Next => base.Next;
        public override Thread Previous => base.Previous;
        internal string typeId => get(Data?.ThreadTypeId);
        internal string processId => get(Data?.ProcessId);
        internal string terminatorId => get(Data?.TerminatorSignatureId);
        internal string threadId => nameOf<TaskData>(x => x.ThreadId);
    }
}
