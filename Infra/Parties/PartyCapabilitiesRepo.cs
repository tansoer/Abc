using Abc.Data.Roles;
using Abc.Domain.Parties.Capabilities;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {
    public sealed class PartyCapabilitiesRepo : EntityRepo<PartyCapability, PartyCapabilityData>,
        IPartyCapabilitiesRepo {
        public PartyCapabilitiesRepo(PartyDb c = null) : base(c, c?.PartyCapabilities) { }
    }
}

