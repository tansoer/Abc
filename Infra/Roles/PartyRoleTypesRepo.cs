using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;

namespace Abc.Infra.Roles {
    public sealed class PartyRoleTypesRepo
        :EntityRepo<PartyRoleType, PartyRoleTypeData>,
             IPartyRoleTypesRepo {
        public PartyRoleTypesRepo(RoleDb c = null)
            : base(c, c?.RoleTypes) { }
    }
}
