using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class ThreadsRepo :EntityRepo<Thread, ThreadData>, IThreadsRepo {
        public ThreadsRepo(ProcessDb c = null) : base(c, c?.Threads) { }
    }
}
