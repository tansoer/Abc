using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class ProcessesRepo :EntityRepo<Process, ProcessData>, IProcessesRepo {
        public ProcessesRepo(ProcessDb c = null) : base(c, c?.Processes) { }
    }
}