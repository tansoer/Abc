using System.Collections.Generic;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Parties {
    public sealed class PersonNamePrefixesPage: ViewFactoryPage<PersonNamePrefixesPage, INamePrefixesRepo, 
        NamePrefix, PersonNamePrefixView, NamePrefixData, PersonNamePrefixViewFactory> {
        protected override string title => PartyTitles.PersonNamePrefixes;
        public PersonNamePrefixesPage(INamePrefixesRepo r) : base(r) {}

        internal IEnumerable<SelectListItem> prefixes;
        internal IEnumerable<SelectListItem> parties;
        public IEnumerable<SelectListItem> Prefixes
            => prefixes ?? selectListByName<IClassifiersRepo, IClassifier, ClassifierData>(x => x.IsTypeOf(ClassifierType.PersonNamePrefix));
        public IEnumerable<SelectListItem> Parties
            => parties ?? selectListByName<IPartyNamesRepo, PartyName, PartyNameData>(null, getName, getId);
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(field => Item.Name)
        };
        protected override void addFields() {
            addField(x => Item.NameId, () => Parties, () => partyName(Item.NameId));
            addField(x => Item.PrefixTypeId, () => Prefixes, () => prefixName(Item.PrefixTypeId));
            addField(x => Item.Index);
            addField(x => Item.Details);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        private static string getId(PartyNameData d) => d?.Id;
        private static string getName(PartyNameData d) => PartyNameFactory.Create(d)?.ToString();
        internal string prefixName(string id) => itemName(Prefixes, id);
        internal string partyName(string id) => itemName(Parties, id);
        protected internal override string pageUrl => PartyUrls.PersonNamePrefixes;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) => Item = new PersonNamePrefixView {
            Name = "N/A",
            Index = (byte)db.NextIndex(fixedValue),
            NameId = fixedValue
        };
    }
}

