
using Abc.Data.Parties;
using Abc.Domain.Parties.Preferences;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {
    public sealed class PreferenceOptionsRepo : EntityRepo<PreferenceOption, PreferenceOptionData>,
        IPreferenceOptionsRepo {
        public PreferenceOptionsRepo(PartyDb c = null) : base(c, c?.PreferenceOptions) { }
    }
}