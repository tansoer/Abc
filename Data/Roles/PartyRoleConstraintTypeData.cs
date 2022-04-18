using Abc.Aids.Enums;
using Abc.Data.Common;

namespace Abc.Data.Roles {
    public sealed class PartyRoleConstraintTypeData : EntityBaseData {
        public PartyType PartyType { get; set; }
        public string OrganizationTypeId { get; set; }
    }
}
