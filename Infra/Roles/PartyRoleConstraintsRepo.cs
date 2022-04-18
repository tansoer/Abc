using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;

namespace Abc.Infra.Roles {
    public sealed class PartyRoleConstraintsRepo
        :EntityRepo<PartyRoleConstraint, PartyRoleConstraintData>,
             IPartyRoleConstraintsRepo {
        public PartyRoleConstraintsRepo(RoleDb c = null)
            : base(c, c?.RoleConstraints) { }
    }
}
