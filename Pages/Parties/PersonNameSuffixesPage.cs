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
    public sealed class PersonNameSuffixesPage : ViewFactoryPage<PersonNameSuffixesPage, INameSuffixesRepo, 
        NameSuffix, PersonNameSuffixView, NameSuffixData, PersonNameSuffixViewFactory> {
        protected override string title => PartyTitles.PersonNameSuffixes;
        public PersonNameSuffixesPage(INameSuffixesRepo r) : base(r) {}
        internal IEnumerable<SelectListItem> suffixes;
        internal IEnumerable<SelectListItem> parties;
        public IEnumerable<SelectListItem> Suffixes
            => suffixes ?? selectListByName<IClassifiersRepo, IClassifier, ClassifierData>(x => x.IsTypeOf(ClassifierType.PersonNameSuffix));
        public IEnumerable<SelectListItem> Parties
            => parties ?? selectListByName<IPartyNamesRepo, PartyName, PartyNameData>(null, getName, getId);
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(field => Item.Name)
        };
        protected override void addFields() {
            addField(x => Item.NameId, () => Parties, () => partyName(Item.NameId));
            addField(x => Item.SuffixTypeId, () => Suffixes, () => suffixName(Item.SuffixTypeId));
            addField(x => Item.Index);
            addField(x => Item.Details);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        private static string getId(PartyNameData d) => d?.Id;
        private static string getName(PartyNameData d) => PartyNameFactory.Create(d)?.ToString();
        internal string suffixName(string id) => itemName(Suffixes, id);
        internal string partyName(string id) => itemName(Parties, id);
        protected internal override string pageUrl => PartyUrls.PersonNameSuffixes;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) => Item = new PersonNameSuffixView {
            Name = "N/A",
            Index = (byte)db.NextIndex(fixedValue),
            NameId = fixedValue
        };
    }
}

