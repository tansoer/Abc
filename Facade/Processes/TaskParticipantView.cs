using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class TaskParticipantView :EntityBaseView {
        [DisplayName("Party Role")] public string PartyRoleId { get; set; }
        [DisplayName("Task")] public string TaskId { get; set; }
    }
}