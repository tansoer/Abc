using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Facade.Common;

namespace Abc.Facade.Roles
{
    public sealed class PartyRoleTypeViewFactory :AbstractViewFactory<PartyRoleTypeData, PartyRoleType, PartyRoleTypeView> {
        protected internal override PartyRoleType toObject(PartyRoleTypeData d) => new(d);
    }
}