using Abc.Data.Roles;

namespace Abc.Data.Processes {
    public sealed class TaskData :PartyRelationshipBaseData, IProcessElementData {
        public string ThreadId { get; set; }
        public string RuleContextId { get; set; }
        public string NextElementId { get; set; }
        public string PreviousElementId { get; set; }
        public string FromPartyAddressId { get; set; }
        public string ToPartyAddressId { get; set; }
        public bool IsEscalation { get; set; }
    }
}
