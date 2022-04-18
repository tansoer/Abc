using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Quantities;

namespace Abc.Domain.Parties.Preferences {

    public sealed class Preference : BasePartyAttribute<PreferenceData> {
        public Preference() : this(null) { }
        public Preference(PreferenceData d) : base(d) { }

        public PreferenceType Type => item<IClassifiersRepo, IClassifier>(typeId) as PreferenceType;
        public PreferenceOption Option => item<IPreferenceOptionsRepo, PreferenceOption>(optionId);
        public Quantity Weight => new Quantity(amount, unit);

        internal string typeId => get(Data?.PreferenceTypeId);
        internal string optionId => get(Data?.PreferenceOptionId);
        internal string unitId => get(Data?.UnitId);
        internal double amount => get(Data?.Weight);
        internal Unit unit => item<IUnitsRepo, Unit>(unitId);
    }
}