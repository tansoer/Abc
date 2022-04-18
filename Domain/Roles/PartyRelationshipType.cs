using Abc.Data.Roles;

namespace Abc.Domain.Roles {
    public sealed class PartyRelationshipType: PartyRelationshipBaseType<PartyRelationshipTypeData> {
        public PartyRelationshipType() : this(null) { }
        public PartyRelationshipType(PartyRelationshipTypeData d) : base(d) { }
    }
}
