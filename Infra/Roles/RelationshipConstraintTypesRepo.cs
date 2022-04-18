using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;

namespace Abc.Infra.Roles {
    public sealed class RelationshipConstraintTypesRepo
        :EntityRepo<RelationshipConstraintType, RelationshipConstraintTypeData>,
             IRelationshipConstraintTypesRepo {
        public RelationshipConstraintTypesRepo(RoleDb c = null)
            : base(c, c?.RelationshipConstraintTypes) { }
    }
}