using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {
    public sealed class PersonNameSuffixViewFactory :
        AbstractViewFactory<NameSuffixData, NameSuffix, PersonNameSuffixView> {

        protected internal override NameSuffix toObject(NameSuffixData d)
            => new NameSuffix(d);
    }
}
