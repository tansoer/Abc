using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;

namespace Abc.Infra.Roles {
    public sealed class PartyRelationshipTypesRepo
        :EntityRepo<PartyRelationshipType, PartyRelationshipTypeData>,
             IPartyRelationshipTypesRepo {
        public PartyRelationshipTypesRepo(RoleDb c = null)
            : base(c, c?.RelationshipTypes) { }
    }
}
