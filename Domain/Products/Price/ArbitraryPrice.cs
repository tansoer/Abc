using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;

namespace Abc.Domain.Products.Price {

    public sealed class ArbitraryPrice : AppliedPrice {

        internal static string priceId => GetMember.Name<PriceEndorsementData>(x => x.PriceId);
        public ArbitraryPrice(PriceData d = null) : base(d) { }
        public IReadOnlyList<PriceEndorsement> Endorsements =>
            new GetFrom<IPriceEndorsementsRepo, PriceEndorsement>().ListBy(priceId, Id);
        public IReadOnlyList<PartySignature> Signatures
            => Endorsements.Select(e => e.PartySignature).ToList().AsReadOnly();

    }

}