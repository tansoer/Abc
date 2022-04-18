using Abc.Data.Parties;

namespace Abc.Domain.Parties.Signatures {

    public sealed class PartySignature : BasePartySignature<PartySignatureData> {

        public PartySignature() : this(null) { }
        public PartySignature(PartySignatureData d) : base(d) { }

    }

}
