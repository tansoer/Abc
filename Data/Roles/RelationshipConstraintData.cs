using Abc.Data.Common;

namespace Abc.Data.Roles {
    public sealed class RelationshipConstraintData : EntityBaseData{
        public string ConstraintTypeId { get; set; }
        public string RelationshipTypeId { get; set; }
    }
}
