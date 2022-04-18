using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;

namespace Abc.Infra.Roles {
    public sealed class RelationshipConstraintsRepo
        :EntityRepo<RelationshipConstraint, RelationshipConstraintData>,
             IRelationshipConstraintsRepo {
        public RelationshipConstraintsRepo(RoleDb c = null)
            : base(c, c?.RelationshipConstraints) { }
    }
}