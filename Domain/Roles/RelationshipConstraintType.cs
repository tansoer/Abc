using Abc.Data.Roles;
using Abc.Domain.Common;

namespace Abc.Domain.Roles {
    public sealed class RelationshipConstraintType : Entity<RelationshipConstraintTypeData>{
        public RelationshipConstraintType() : this(null) { }
        public RelationshipConstraintType(RelationshipConstraintTypeData d) : base(d) { }
        internal string consumerPartyRoleTypeId => get(Data?.ConsumerRoleTypeId);
        internal string providerPartyRoleTypeId => get(Data?.ProviderRoleTypeId);
        public bool CanFormRelationship(PartyRole consumer, PartyRole provider) {
            if (consumer?.typeId != consumerPartyRoleTypeId) return false;
            return (provider?.typeId == providerPartyRoleTypeId);
        }
    }
}
