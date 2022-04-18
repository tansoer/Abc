using Abc.Data.Parties;
using Abc.Data.Roles;
using Abc.Domain.Parties;
using Abc.Domain.Roles;
using Abc.Facade.Roles;
using Abc.Pages.Common;
using Abc.Pages.Parties;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Roles {

    //TODO: Is not implemented
    public sealed class RolesPage : ViewFactoryPage<RolesPage, IPartyRolesRepo, PartyRole, PartyRoleView, PartyRoleData, PartyRoleViewFactory> {
        public RolesPage(IPartyRolesRepo r, IPartiesRepo p) :base(r) {
            parties = p;
        }
        internal IPartiesRepo parties;
        internal IEnumerable<SelectListItem> partiesList;
        protected override string title => PartyTitles.Roles;
        protected internal override string pageUrl => PartyUrls.Roles;
        protected override void addFields() {
            //addField(x => Item.PartyRoleTypeId);
            addField(x => Item.PartyId, () => Parties, () => partyName(Item.PartyId));
            addField(x => Item.Name);
            addField(x => Item.Details);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        public IEnumerable<SelectListItem> Parties
            => partiesList ?? selectListByName<IPartiesRepo, IParty, PartyData>( null, getName, getId);
        private static string getId(PartyData d) => d?.Id;
        private static string getName(PartyData d) => PartyFactory.Create(d)?.GetName();
        private string partyName(string id) => itemName(Parties, id);
    }
}