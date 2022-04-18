using System.Collections.Generic;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;

namespace Abc.Domain.Parties.Preferences {
    public sealed class PreferenceType : Classifier<PreferenceType> {

        public PreferenceType() : this(null) { }
        public PreferenceType(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.PartyPreference;

        public IReadOnlyList<PreferenceOption> Options => list<IPreferenceOptionsRepo, PreferenceOption>(typeId, Id);

        internal static string typeId => nameOf<PreferenceOptionData>(x => x.PreferenceTypeId);
    }
}