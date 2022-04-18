using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class TaskParticipantsRepo :EntityRepo<TaskParticipant, TaskParticipantData>, ITaskParticipantsRepo {
        public TaskParticipantsRepo(ProcessDb c = null) : base(c, c?.TaskParticipants) { }
    }
}