using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class ProcessView : EntityBaseView {
        [DisplayName("Rule Context")] public string RuleContextId { get; set; }
        [DisplayName("Process Type")] public string ProcessTypeId { get; set; }
        [DisplayName("Manager")] public string ManagerPartyRoleId { get; set; }
        [DisplayName("Initiator")] public string InitiatorPartyRoleId { get; set; }
        [DisplayName("Priority")] public string PriorityClassifierId { get; set; }


    }
}
