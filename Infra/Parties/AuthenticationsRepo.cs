using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class AuthenticationsRepo :
        EntityRepo<Authentication, AuthenticationData>,
        IAuthenticationsRepo {
        public AuthenticationsRepo(PartyDb c = null) : base(c, c?.PartyAuthentications) { }
    }
}

