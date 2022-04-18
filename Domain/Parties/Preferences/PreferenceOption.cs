using Abc.Data.Parties;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Preferences {

    public sealed class PreferenceOption : Entity<PreferenceOptionData> {

        public PreferenceOption() : this(null) { }
        public PreferenceOption(PreferenceOptionData d) : base(d) { }

        internal string typeId => get(Data?.PreferenceTypeId);
    }
}