using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Identifiers;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Parties {
    public sealed class RegisteredIdentifiersPage :ViewFactoryPage<RegisteredIdentifiersPage, IRegisteredIdentifiersRepo,
        RegisteredIdentifier, RegisteredIdentifierView, RegisteredIdentifierData, RegisteredIdentifierViewFactory> {
        protected override string title => PartyTitles.RegisteredIdentifiers;
        protected internal override string subtitle => $"{partyName(FixedValue)}";
        protected internal override IPageUrl masterPage() => new PartyPage(null, null, null, null,null, null, null, null, null );
        internal IPartyNamesRepo names;
        internal IClassifiersRepo classifiers;
        public RegisteredIdentifiersPage(IRegisteredIdentifiersRepo r
            , IPartyNamesRepo n, IClassifiersRepo c) : base(r) {
            names = n;
            classifiers = c;
        }
        internal IEnumerable<SelectListItem> identifiers;
        internal IEnumerable<SelectListItem> authorities;
        internal IEnumerable<SelectListItem> parties;
        public IEnumerable<SelectListItem> Identifiers
            => identifiers ?? selectListByName<IClassifiersRepo, IClassifier, ClassifierData>( x => x.IsTypeOf(ClassifierType.RegisteredIdentifier));
        public IEnumerable<SelectListItem> Authorities
            => authorities ?? selectListByName<IClassifiersRepo, IClassifier, ClassifierData>(x => x.IsTypeOf(ClassifierType.RegistrationAuthority));
        public IEnumerable<SelectListItem> Parties
            => parties ?? selectListByName<IPartyNamesRepo, PartyName, PartyNameData>(null, getName, getId);
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void addFields() {
            addField(x => Item.PartyId, () => Parties, () => partyName(Item.PartyId));
            addField(x => Item.RegisteredIdentifierTypeId, () => Identifiers, () => identifierTypeName(Item.RegisteredIdentifierTypeId));
            addField(x => Item.Name);
            addField(x => Item.RegistrationAuthorityId, () => Authorities, () => authorityName(Item.RegistrationAuthorityId));
            addField(x => Item.Details);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        private static string getId(PartyNameData d) => d?.PartyId;
        private static string getName(PartyNameData d) => PartyNameFactory.Create(d)?.ToString();
        private string authorityName(string id) => itemName(Authorities, id);
        private string identifierTypeName(string id) => itemName(Identifiers, id);
        private string partyName(string id) => itemName(Parties, id);
        protected internal override string pageUrl => PartyUrls.RegisteredIdentifiers;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) => 
            Item = new RegisteredIdentifierView { PartyId = fixedFilter };
    }
}
