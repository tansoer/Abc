using Abc.Data.Roles;

namespace Abc.Domain.Roles {
    public sealed class PartyRelationship: PartyBaseRelationship<PartyRelationshipData> {
        public PartyRelationship() : this(null) { }
        public PartyRelationship(PartyRelationshipData d) : base(d) { }
        public PartyRelationshipType Type => item<IPartyRelationshipTypesRepo, PartyRelationshipType>(RelationshipTypeId);
    }
}
