using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class TaskParticipantViewFactory :AbstractViewFactory<TaskParticipantData, TaskParticipant, TaskParticipantView> {
        protected internal override TaskParticipant toObject(TaskParticipantData d) => new(d);
    }
}
