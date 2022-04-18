using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;

namespace Abc.Infra.Roles {
    public sealed class PartyRoleConstraintTypesRepo
        :EntityRepo<PartyRoleConstraintType, PartyRoleConstraintTypeData>,
             IPartyRoleConstraintTypesRepo {
        public PartyRoleConstraintTypesRepo(RoleDb c = null)
            : base(c, c?.RoleConstraintTypes) { }
    }
}
