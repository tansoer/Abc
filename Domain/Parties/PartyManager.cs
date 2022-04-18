using Abc.Domain.Common;
using Abc.Domain.Parties.Identifiers;
using Abc.Domain.Parties.Names;
using Abc.Domain.Roles;
using System.Collections.Generic;

namespace Abc.Domain.Parties {
    public sealed class PartyManager: Manager<IPartiesRepo, IParty> {
        public IReadOnlyList<PartyName> FindByName(string searchString) 
            => find<IPartyNamesRepo, PartyName>(searchString);
        public IReadOnlyList<RegisteredIdentifier> FindByRegisteredId(string searchString) => 
            find<IRegisteredIdentifiersRepo,RegisteredIdentifier>(searchString);
        public IReadOnlyList<IPartySummary> FindBySummary(string searchString) => 
            find<IPartySummariesRepo, IPartySummary>(searchString);
        public IParty FindByName(IPartyName n) => FindById(n.PartyId);
        public IParty FindByRegisteredId(RegisteredIdentifier id) => FindById(id.PartyId);
        public IParty FindBySummary(PartySummary s) => FindById(s.PartyId);
        public IParty FindByRole(PartyRole r) => FindById(r.PartyId);
    }
}
