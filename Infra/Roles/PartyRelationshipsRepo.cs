using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;

namespace Abc.Infra.Roles {
    public sealed class PartyRelationshipsRepo
        :EntityRepo<PartyRelationship, PartyRelationshipData>,
             IPartyRelationshipsRepo {
        public PartyRelationshipsRepo(RoleDb c = null)
            : base(c, c?.Relationships) { }
    }
}
