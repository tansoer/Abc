using Abc.Data.Common;

namespace Abc.Data.Roles {
    public sealed class AssignedResponsibilityData :EntityBaseData, IPartyRoleAttributeData {
        public string PartyRoleId { get; set; }
        public string ResponsibilityId { get; set; }
        public string PartySignatureId { get; set; }
    }
}
