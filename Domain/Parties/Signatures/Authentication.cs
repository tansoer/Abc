using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;

namespace Abc.Domain.Parties.Signatures {

    public sealed class Authentication : BasePartyAttribute<AuthenticationData> {
        public Authentication() : this(null) { }
        public Authentication(AuthenticationData d) : base(d) { }
        public string PartyUserId => get(Data?.PartyUserId);
        public string Token => get(Data?.Token);
        public string AuthenticationProvider => get(Data?.AuthenticationProvider);
    }
}