using Abc.Data.Roles;
using Abc.Domain.Common;

namespace Abc.Domain.Roles {
    public abstract class PartyBaseRelationship<TData> :Relationship<TData> 
        where TData: PartyRelationshipBaseData, new() {
        protected PartyBaseRelationship() : this(null) { }
        protected PartyBaseRelationship(TData d) : base(d) { }
        public PartyRole Consumer => item<IPartyRolesRepo, PartyRole>(ConsumerEntityId);
        public PartyRole Provider => item<IPartyRolesRepo, PartyRole>(ProviderEntityId);
    }
}
