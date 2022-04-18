using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Products.Price;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Price {

    [TestClass] public class ArbitraryPriceTests :SealedTests<ArbitraryPrice, AppliedPrice> {
        [TestMethod] public async Task EndorsementsTest() =>
            await testListAsync<PriceEndorsement, PriceEndorsementData, IPriceEndorsementsRepo>(
                x => x.PriceId = obj.Id, d => new PriceEndorsement(d));
        [TestMethod] public void SignaturesTest() =>
            testRelatedList<PartySignature, PartySignatureData, PriceEndorsement, IPartySignaturesRepo>(
                () => obj.Signatures, () => obj.Endorsements,
                (d, e) => {
                    d.Id = e.PartySignatureId;
                    return new PartySignature(d);
                }, EndorsementsTest, (x, e) => x.Id == e.PartySignatureId);
    }
}