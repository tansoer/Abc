using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Facade.Common;

namespace Abc.Facade.Roles {
    public sealed class PartyRelationshipViewFactory : AbstractViewFactory<PartyRelationshipData, PartyRelationship, PartyRelationshipView> {
        protected internal override PartyRelationship toObject(PartyRelationshipData d) => new(d);
    }
}