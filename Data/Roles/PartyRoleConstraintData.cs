using Abc.Data.Common;

namespace Abc.Data.Roles {
    public sealed class PartyRoleConstraintData : EntityBaseData, IPartyRoleTypeAttributeData  {
        public string RoleConstraintTypeId { get; set; }
        public string PartyRoleTypeId { get; set; }
    }
}
