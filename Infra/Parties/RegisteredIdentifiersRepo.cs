using Abc.Data.Parties;
using Abc.Domain.Parties.Identifiers;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class RegisteredIdentifiersRepo :
        EntityRepo<RegisteredIdentifier, RegisteredIdentifierData>,
        IRegisteredIdentifiersRepo {
        public RegisteredIdentifiersRepo(PartyDb c = null) : base(c, c?.Identifiers) { }
    }
}
