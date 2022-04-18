using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Roles;

namespace Abc.Domain.Processes {
    public sealed class TaskParticipant :Entity<TaskParticipantData> {
        public TaskParticipant() : this(null) { }
        public TaskParticipant(TaskParticipantData d) : base(d) { }
        public PartyRole PartyRole => item<IPartyRolesRepo, PartyRole>(partyRoleId);
        internal string partyRoleId => get(Data?.PartyRoleId);
        public string taskId => get(Data?.TaskId);
    }
}
