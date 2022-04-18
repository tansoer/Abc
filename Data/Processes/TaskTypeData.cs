using Abc.Data.Roles;

namespace Abc.Data.Processes {
    public sealed class TaskTypeData : PartyRelationshipBaseTypeData, IProcessElementTypeData {
        public string ThreadTypeId { get; set; }
        public string NextElementId { get; set; }
        public string PreviousElementId { get; set; }
    }
}
