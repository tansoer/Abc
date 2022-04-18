using Abc.Facade.Roles;
using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class TaskView :PartyRelationshipBaseView {
        [DisplayName("Thread")] public string ThreadId { get; set; }
        [DisplayName("Rule context")] public string RuleContextId { get; set; }
        [DisplayName("Next element")] public string NextElementId { get; set; }
        [DisplayName("Previous element")] public string PreviousElementId { get; set; }
        [DisplayName("From party")] public string FromPartyAddressId { get; set; }
        [DisplayName("To party")] public string ToPartyAddressId { get; set; }
        [DisplayName("Is escalation")] public bool IsEscalation { get; set; }
    }

}
