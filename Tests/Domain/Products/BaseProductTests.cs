using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Features;
using Abc.Domain.Products.Packets;
using Abc.Domain.Products.Price;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {
    [TestClass] public class BaseProductTests :AbstractTests<BaseProduct<ProductType>, Entity<ProductData>> {
        private class testClass :BaseProduct<ProductType> {
            public testClass(ProductData d) :base(d) { }
            public override ProductType Type => type as ProductType;
        }
        protected override BaseProduct<ProductType> createObject() => new testClass(GetRandom.ObjectOf<ProductData>());
        [TestMethod] public async Task TypeTest() =>
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                obj.TypeId, () => obj.type.Data, ProductTypeFactory.Create);
        [TestMethod] public void ProductKindTest() => isReadOnly(obj.Data.ProductKind);
        [TestMethod] public void SerialNumberTest() => isReadOnly(obj.Data.Code);
        [TestMethod] public void ReservationStatusTest() => isReadOnly(obj.Data.ReservationStatus);
        [TestMethod] public void TypeIdTest() => isReadOnly(obj.Data.ProductTypeId);
        [TestMethod] public void BatchIdTest() => isReadOnly(obj.Data.BatchId);
        [TestMethod] public async Task BatchTest() =>
            await testItemAsync<BatchData, Batch, IBatchesRepo>(
                obj.BatchId, () => obj.Batch.Data, d => new Batch(d));
        [TestMethod] public async Task FeaturesTest()
            => await testListAsync<Feature, FeatureData, IFeaturesRepo>(
                x => x.ProductId = obj.Id, d => new Feature(d));
        [TestMethod] public async Task PriceTest() {
            isReadOnly();
            await RelatedPricesTest();
            var actual = obj.Price.Data;
            var expected = obj.RelatedPrices[0].Data;
            allPropertiesAreEqual(expected, actual);
        }
        [TestMethod] public async Task RelatedPricesTest()
            => await testListAsync<IPrice, PriceData, IPricesRepo>(
                x => {
                    x.ProductId = obj.Id;
                    x.Kind = rndBool ? PriceKind.Agreed : PriceKind.Arbitrary;
                },
                PriceFactory.Create);
        [TestMethod] public async Task RelationsWhereConsumerTest()
            => await testListAsync<ProductRelationship, ProductRelationshipData, IProductRelationshipsRepo>(
                x => x.ConsumerEntityId = obj.Id, d => new ProductRelationship(d));
        [TestMethod] public async Task RelationsWhereProviderTest()
            => await testListAsync<ProductRelationship, ProductRelationshipData, IProductRelationshipsRepo>(
                x => x.ProviderEntityId = obj.Id, d => new ProductRelationship(d));
        [TestMethod] public void ConsumerProductsTest() =>
            testRelatedList<IProduct, ProductData, ProductRelationship, IProductsRepo>(
                () => obj.ConsumerProducts, () => obj.RelationsWhereConsumer,
                (d, e) => { d.Id = e.ProviderEntityId;
                    return ProductFactory.Create(d);
                }, RelationsWhereConsumerTest, (x, e)=> x.Id == e.ProviderEntityId
            );
        [TestMethod] public void ProviderProductsTest() =>
            testRelatedList<IProduct, ProductData, ProductRelationship, IProductsRepo>(
                () => obj.ProviderProducts, () => obj.RelationsWhereProvider,
                (d, e) => { 
                    d.Id = e.ConsumerEntityId;
                    return ProductFactory.Create(d);
                }, RelationsWhereProviderTest, (x, p) => x.Id == p.ConsumerEntityId);
        [TestMethod] public async Task RelationsTest() {
            isReadOnly();
            await RelationsWhereProviderTest();
            await RelationsWhereConsumerTest();
            Assert.AreEqual(obj.RelationsWhereProvider.Count + obj.RelationsWhereConsumer.Count, obj.Relations.Count);

            foreach (var e in obj.RelationsWhereProvider) {
                var o = obj.Relations.First(x => x.Id == e.Id);
                allPropertiesAreEqual(o.Data, e.Data);
            }

            foreach (var e in obj.RelationsWhereConsumer) {
                var o = obj.Relations.First(x => x.Id == e.Id);
                allPropertiesAreEqual(o.Data, e.Data);
            }
        }
        [TestMethod] public void RelatedProductsTest() {
            isReadOnly();
            ConsumerProductsTest();
            ProviderProductsTest();
            Assert.AreEqual(obj.ConsumerProducts.Count + obj.ProviderProducts.Count, obj.RelatedProducts.Count);

            foreach (var e in obj.ConsumerProducts) {
                var o = obj.RelatedProducts.First(x => x.Id == e.Id);
                allPropertiesAreEqual(o.Data, e.Data);
            }

            foreach (var e in obj.ProviderProducts) {
                var o = obj.RelatedProducts.First(x => x.Id == e.Id);
                allPropertiesAreEqual(o.Data, e.Data);
            }
        }
    }
}