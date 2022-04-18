using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Products.Price;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Price {

    [TestClass]
    public class PriceEndorsementTests : SealedTests<PriceEndorsement, Entity<PriceEndorsementData>> {
        protected override PriceEndorsement createObject() => new (random<PriceEndorsementData>());
        [TestMethod] public void PriceIdTest() => isReadOnly(obj.Data.PriceId);
        [TestMethod] public void PartySignatureIdTest() => isReadOnly(obj.Data.PartySignatureId);
        [TestMethod] public async Task PriceTest() =>
            await testItemAsync<PriceData, IPrice, IPricesRepo>(
                obj.PriceId, () => obj.Price.Data, PriceFactory.Create);
        [TestMethod] public async Task PartySignatureTest() =>
            await testPartyAttributeAsync<PartySignatureData, PartySignature, IPartySignaturesRepo>(
                obj.PartySignatureId, () => obj.PartySignature.Data, d => new PartySignature(d));
    }
}