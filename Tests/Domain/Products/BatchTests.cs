using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {

    [TestClass] public class BatchTests :SealedTests<Batch, Entity<BatchData>> {
        protected override Batch createObject() => new(GetRandom.ObjectOf<BatchData>());
        [TestMethod] public void typeIdTest() => isReadOnly(obj.Data.ProductTypeId);
        [TestMethod] public void FirstSerialNumberTest() => isReadOnly(obj.Data.FirstSerialNumber);
        [TestMethod] public void LastSerialNumberTest() => isReadOnly(obj.Data.LastSerialNumber);
        [TestMethod] public void SellByTest() => isReadOnly(obj.Data.SellBy);
        [TestMethod] public void UseByTest() => isReadOnly(obj.Data.UseBy);
        [TestMethod] public void BestBeforeTest() => isReadOnly(obj.Data.BestBefore);
        [TestMethod] public void DateProducedTest() => isReadOnly(obj.Data.DateProduced);

        [TestMethod] public async Task BatchOfTest() {
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                obj.typeId, () => obj.BatchOf.Data, ProductTypeFactory.Create);
        }

        [TestMethod] public async Task ProductsTest() {
            await testListAsync<IProduct, ProductData, IProductsRepo>(
                d => d.BatchId = obj.Id, ProductFactory.Create);
        }

        [TestMethod] public void ProductsCountTest() {
            isReadOnly(obj.Products.Count);
        }

        [TestMethod] public async Task CheckedByTest() {
            isReadOnly();
            Assert.AreEqual(0, obj.checkedBy.Count);
            await checkedByTest();
            foreach (var e in obj.checkedBy) {
                var d = GetRandom.ObjectOf<PartySignatureData>();
                d.Id = e.PartySignatureId;
                await addAsync<IPartySignaturesRepo, PartySignature>(new PartySignature(d));
            }

            Assert.AreNotEqual(0, obj.CheckedBy.Count);
            Assert.AreEqual(obj.checkedBy.Count, obj.CheckedBy.Count);
        }

        [TestMethod] public async Task checkedByTest() {
            await testListAsync<BatchCheckedBy, BatchCheckedByData, IBatchCheckedByRepo>(
                obj, nameof(obj.checkedBy),
                x => x.BatchId = obj.Id,
                d => new BatchCheckedBy(d));
        }
    }
}