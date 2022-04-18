using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Facade.Common;

namespace Abc.Facade.Roles {
    public sealed class PartyRelationshipTypeViewFactory :AbstractViewFactory<PartyRelationshipTypeData, PartyRelationshipType, PartyRelationshipTypeView> {
        protected internal override PartyRelationshipType toObject(PartyRelationshipTypeData d) => new(d);
    }
}