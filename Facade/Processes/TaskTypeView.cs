using Abc.Facade.Roles;
using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class TaskTypeView :PartyRelationshipBaseTypeView {
        [DisplayName("Thread type")] public string ThreadTypeId { get; set; }
        [DisplayName("Next element")] public string NextElementId { get; set; }
        [DisplayName("Previous element")] public string PreviousElementId { get; set; }
    }

}
