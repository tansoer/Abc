using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {
    public sealed class PartySignaturesRepo : EntityRepo<PartySignature, PartySignatureData>,
        IPartySignaturesRepo {
        public PartySignaturesRepo(PartyDb c = null) : base(c, c?.PartySignatures) { }
    }
}

