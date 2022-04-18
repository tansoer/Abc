using Abc.Data.Common;

namespace Abc.Data.Processes {
    public sealed class TaskParticipantData :EntityBaseData {
        public string PartyRoleId { get; set; }
        public string TaskId { get; set; }
    }
}
