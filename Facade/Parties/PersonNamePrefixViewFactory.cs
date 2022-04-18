using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {
    public sealed class PersonNamePrefixViewFactory :
        AbstractViewFactory<NamePrefixData, NamePrefix, PersonNamePrefixView> {
        protected internal override NamePrefix toObject(NamePrefixData d)
            => new NamePrefix(d);
    }
}
