using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class ActionTypesRepo :EntityRepo<ActionType, ActionTypeData>, IActionTypesRepo {
        public ActionTypesRepo(ProcessDb c = null) : base(c, c?.ActionTypes) { }
    }
}