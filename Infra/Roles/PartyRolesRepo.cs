using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;

namespace Abc.Infra.Roles {
    public sealed class PartyRolesRepo : EntityRepo<PartyRole, PartyRoleData>, IPartyRolesRepo {
        public PartyRolesRepo(RoleDb c = null) : base(c, c?.Roles) { }
    }
}
