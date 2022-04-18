using Abc.Data.Parties;
using Abc.Domain.Parties.Identifiers;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class RegistrationAuthoritiesRepo :
        EntityRepo<RegistrationAuthority, RegistrationAuthorityData>,
        IRegistrationAuthoritiesRepo {
        public RegistrationAuthoritiesRepo(PartyDb c = null) : base(c, c?.RegistrationAuthorities) { }
    }
}