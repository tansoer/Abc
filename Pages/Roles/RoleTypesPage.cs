using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Facade.Roles;
using Abc.Pages.Common;
using Abc.Pages.Parties;

namespace Abc.Pages.Roles {
    public sealed class RoleTypesPage :ViewFactoryPage<RoleTypesPage, IPartyRoleTypesRepo, PartyRoleType, PartyRoleTypeView, PartyRoleTypeData, PartyRoleTypeViewFactory> {
        public RoleTypesPage(IPartyRoleTypesRepo r) : base(r) { }
        protected override string title => PartyTitles.RoleTypes;
        protected internal override string pageUrl => PartyUrls.RolesTypes;
        protected override void addFields() {
            addField(x => Item.BaseTypeId);
            addField(x => Item.RuleSetId);
            addField(x => Item.Name);
            addField(x => Item.Code);
            addField(x => Item.Details);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
    }
}
