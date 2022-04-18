using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class ProcessTypesRepo :EntityRepo<ProcessType, ProcessTypeData>, IProcessTypesRepo {
        public ProcessTypesRepo(ProcessDb c = null) : base(c, c?.ProcessTypes) { }
    }
}