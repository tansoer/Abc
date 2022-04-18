using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {

    public sealed class PartyNameViewFactory: 
        AbstractViewFactory<PartyNameData, PartyName, PartyNameView > {

        protected internal override PartyName toObject(PartyNameData d)
            => PartyNameFactory.Create(d);
    }
}
