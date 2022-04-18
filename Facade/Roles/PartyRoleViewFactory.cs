using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Facade.Common;

namespace Abc.Facade.Roles
{
    public sealed class PartyRoleViewFactory : AbstractViewFactory<PartyRoleData, PartyRole, PartyRoleView> {
        protected internal override PartyRole toObject(PartyRoleData d) => new(d);
    }
}