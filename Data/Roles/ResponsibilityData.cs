using Abc.Data.Common;

namespace Abc.Data.Roles {
    public sealed class ResponsibilityData: EntityBaseData, IPartyRoleTypeAttributeData {
        public bool IsOptional { get; set; }
        public string ResponsibilityTypeId { get; set; }
        public string PartyRoleTypeId { get; set; }
    }
}
