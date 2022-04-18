using Abc.Data.Parties;
using Abc.Domain.Parties.Preferences;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {
    public sealed class PreferenceViewFactory :
        AbstractViewFactory<PreferenceData, Preference, PreferenceView> {
        protected internal override Preference toObject(PreferenceData d)
            => new Preference(d);
    }
}
