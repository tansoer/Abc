using Abc.Data.Common;

namespace Abc.Data.Roles {
    public sealed class RelationshipConstraintTypeData : EntityBaseData{
        public string ConsumerRoleTypeId { get; set; }
        public string ProviderRoleTypeId { get; set; }
    }
}
