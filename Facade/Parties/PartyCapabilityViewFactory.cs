using Abc.Data.Roles;
using Abc.Domain.Parties.Capabilities;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {
    public sealed class PartyCapabilityViewFactory :
        AbstractViewFactory<PartyCapabilityData, PartyCapability, PartyCapabilityView> {
        protected internal override PartyCapability toObject(PartyCapabilityData d) 
            => new PartyCapability(d);
    }
}
