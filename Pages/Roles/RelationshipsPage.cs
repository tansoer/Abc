using Abc.Data.Roles;
using Abc.Domain.Parties.Names;
using Abc.Domain.Roles;
using Abc.Facade.Roles;
using Abc.Pages.Common;
using Abc.Pages.Parties;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Roles {
    public sealed class RelationshipsPage :ViewFactoryPage<RelationshipsPage, IPartyRelationshipsRepo, PartyRelationship, PartyRelationshipView, PartyRelationshipData, PartyRelationshipViewFactory> {
        public RelationshipsPage(IPartyRelationshipsRepo r, IPartyRolesRepo ro, IPartyNamesRepo n) :base(r) {
            roles = ro;
            names = n;
        }
        internal IPartyRolesRepo roles;
        internal IPartyNamesRepo names;
        internal IEnumerable<SelectListItem> entities;
        protected override string title => PartyTitles.Relationships;
        protected internal override string pageUrl => PartyUrls.Relationships;
        public IEnumerable<SelectListItem> Entities
            => entities ?? selectListByName<IPartyRolesRepo, PartyRole, PartyRoleData>( null, getName, getId);
        protected override void addFields() {
            addField(x => Item.Name);
            addField(x => Item.ProviderEntityId, () => Entities, () => partyName(Item.ProviderEntityId));
            addField(x => Item.ConsumerEntityId, () => Entities, () => partyName(Item.ConsumerEntityId));
            addField(x => Item.Details);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        private static string getId(PartyRoleData d) => d?.PartyId;
        private static string getName(PartyRoleData d) => new PartyRole(d).Party.GetName();
        private string partyName(string id) => itemName(Entities, id);
    }
}