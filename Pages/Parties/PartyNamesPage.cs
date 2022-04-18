using System;
using Abc.Aids.Enums;
using Abc.Data.Parties;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using System.Collections.Generic;

namespace Abc.Pages.Parties {
    public sealed class PartyNamesPage : ViewsFactoryPage<PartyNamesPage, IPartyNamesRepo, 
        PartyName, PartyNameView, PartyNameData, PartyNameViewFactory, PartyType> {
        internal IPartiesRepo parties;
        protected override string title => PartyTitles.Names;
        protected internal override string subtitle => $"{PartyName(FixedValue)}";
        public PartyNamesPage(IPartyNamesRepo r, IPartiesRepo p) : base(r) => parties = p;
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.PartyType),
            field(x => Item.Id),
            field(x => Item.PartyId)
        };
        protected override void addFields() {
            addField(x => Item.PartyType);
            addField(x => Item.Name,  () => Item.ToString());
            addField(x => Item.NameType);
        }
        protected override void fieldsForViewers() {
            removeField(x => Item.Name);
            if (Item.IsPersonName()) {
                addFieldBefore(field(x => Item.Name, PartyNameView.FamilyNameCaption), x => Item.NameType);
                addFieldBefore(field(x => Item.GivenName), x => Item.NameType);
                addFieldBefore(field(x => Item.MiddleName), x => Item.NameType);
                addFieldBefore(field(x => Item.PreferredName), x => Item.NameType);
            } else addFieldBefore(field(x => Item.Name), x => Item.NameType);
        }
        protected override void fieldsForEditors() {
            fieldsForViewers();
            removeField(x => Item.PartyType);
        }
        protected internal override string pageUrl => PartyUrls.Names;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => createItem(fixedValue, switchOfCreate ?? 0);
        private void createItem(string partyId, int switchOfCreate) {
            Item = new() {
                Id = Guid.NewGuid().ToString(),
                PartyId = partyId,
                PartyType = partyNameType(switchOfCreate)
            };
        }
        internal static PartyType partyNameType(int i) =>
            Enum.IsDefined(typeof(PartyType), i)
            ? (PartyType)i
            : PartyType.Unspecified;
        private string PartyName(string id) => parties.Get(id).GetName();
    }
}
