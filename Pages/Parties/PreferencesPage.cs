using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Data.Quantities;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Preferences;
using Abc.Domain.Quantities;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Abc.Pages.Parties {
    public sealed class PreferencesPage :ViewFactoryPage<PreferencesPage, IPreferencesRepo,
        Preference, PreferenceView, PreferenceData, PreferenceViewFactory> {
        public PreferencesPage(IPreferencesRepo r) : base(r) {
        }
        protected override string title => PartyTitles.Preferences;
        protected internal override string subtitle => PartyName(FixedValue);
        private IEnumerable<SelectListItem> preferenceTypes;
        private IEnumerable<SelectListItem> parties;
        private IEnumerable<SelectListItem> units;
        private IEnumerable<SelectListItem> options;
        public IEnumerable<SelectListItem> PreferenceTypes => preferenceTypes ??= selectListByName<IClassifiersRepo, IClassifier, ClassifierData>(x => x.Data.ClassifierType == ClassifierType.PartyPreference);
        public IEnumerable<SelectListItem> Parties=>parties ??= selectListById<IPartiesRepo, IParty, PartyData>(null, x => x?.GetName() ?? x?.Id);
        public IEnumerable<SelectListItem> Units => units ??= selectListByName<IUnitsRepo, Unit, UnitData>();
        public IEnumerable<SelectListItem> Options => options ??= selectListByName<IPreferenceOptionsRepo, PreferenceOption, PreferenceOptionData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(x => Item.Name)
        };
        protected override void addFields() {
            addField(x => Item.PartyId, () => Parties, () => PartyName(Item.PartyId));
            addField(x => Item.PreferenceTypeId, () => PreferenceTypes, () => PreferenceName(Item.PreferenceTypeId));
            addField(x => Item.Weight);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override List<ExpressionHelper> getFieldsForViewers() {
            var helpers = Helpers.GetRange(0, 2);
            if (FixedFilter != null) helpers.RemoveAt(0);
            helpers.Add(field(x => Item.UnitId, () => Units, () => UnitName(Item.UnitId)));
            helpers.Add(field(x => Item.PreferenceOptionId, () => Options, () => OptionName(Item.PreferenceOptionId)));
            helpers.Add(field(x => Item.PartyRoleId));
            helpers.AddRange(Helpers.GetRange(2, 3));
            return helpers;
        }
        protected override List<ExpressionHelper> getFieldsForEditors() => getFieldsForViewers();
        protected internal override string pageUrl => PartyUrls.Preferences;
        public string PartyName(string id) => itemName(Parties, id);
        public string PreferenceName(string id) => itemName(PreferenceTypes, id);
        public string UnitName(string id) => itemName(Units, id);
        public string OptionName(string id) => itemName(Options, id);
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => createItem(fixedValue);
        private void createItem(string partyId) => Item = new PreferenceView {
            Id = Guid.NewGuid().ToString(),
            PartyId = partyId,
            Name = "N/A"
        };
    }
}
