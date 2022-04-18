using Abc.Data.Parties;
using Abc.Domain.Parties.Preferences;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class PreferencesRepo :EntityRepo<Preference, PreferenceData>,
        IPreferencesRepo {
        public PreferencesRepo(PartyDb c = null) : base(c, c?.Preferences) { }
    }
}