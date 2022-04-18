using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;

namespace Abc.Domain.Products.Price {

    public sealed class PriceEndorsement : Entity<PriceEndorsementData> {
        public PriceEndorsement() : this(null) { }
        public PriceEndorsement(PriceEndorsementData d) : base(d) { }
        public string PriceId => Data?.PriceId ?? Unspecified.String;
        public string PartySignatureId => Data?.PartySignatureId ?? Unspecified.String;
        public IPrice Price =>
            new GetFrom<IPricesRepo, IPrice>().ById(PriceId);
        public PartySignature PartySignature =>
            new GetFrom<IPartySignaturesRepo, PartySignature>().ById(PartySignatureId);
    }
}