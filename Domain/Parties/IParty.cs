
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Capabilities;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using System.Collections.Generic;

namespace Abc.Domain.Parties {

    public interface IParty : IEntity<PartyData> {
        string GetName();
        public IReadOnlyList<PartyCapability> Capabilities { get; }
        public bool IsCapable(IRuleSet requirements);
        public bool IsCapable(IReadOnlyList<Responsibility> responsibilities);
    }
    public interface IParty<TName> : IParty {
        TName OfficialName { get; }
    }
}
