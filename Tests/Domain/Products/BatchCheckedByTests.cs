using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {

    [TestClass]
    public class BatchCheckedByTests : SealedTests<BatchCheckedBy, Entity<BatchCheckedByData>> {
        protected override BatchCheckedBy createObject() => new (GetRandom.ObjectOf<BatchCheckedByData>());
        [TestMethod]
        public void BatchIdTest() => isReadOnly(obj.Data.BatchId);

        [TestMethod]
        public void PartySignatureIdTest() => isReadOnly(obj.Data.PartySignatureId);

        [TestMethod] public async Task BatchTest() {
            await testItemAsync<BatchData, Batch, IBatchesRepo>(
                obj.BatchId, () => obj.Batch.Data, d => new Batch(d));
        }

        [TestMethod] public async Task PartySignatureTest() {

            await testPartyAttributeAsync<PartySignatureData, PartySignature, IPartySignaturesRepo>(
                obj.PartySignatureId, () => obj.PartySignature.Data, d => new PartySignature(d));
        }
    }

}