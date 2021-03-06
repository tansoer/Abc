using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class ActionsRepo : EntityRepo<Action, ActionData>, IActionsRepo {
        public ActionsRepo(ProcessDb c = null) : base(c, c?.Actions) { }
    }
}