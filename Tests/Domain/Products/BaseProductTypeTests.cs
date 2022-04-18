using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Features;
using Abc.Domain.Products.Packets;
using Abc.Domain.Products.Price;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Products {

    [TestClass]
    public class BaseProductTypeTests : AbstractTests<BaseProductType, Entity<ProductTypeData>> {

        private class testClass : BaseProductType {
            public testClass(ProductTypeData d) : base(d) { }
        }
        protected override BaseProductType createObject() => new testClass(GetRandom.ObjectOf<ProductTypeData>());
        
        [TestMethod] public void AlternativeCodesTest() => isReadOnly(obj.Data.AlternativeCodes);
        [TestMethod] public void IsBaseTypeTest() => isReadOnly(obj.Data.IsBaseType);
        [TestMethod] public void DescriptionTest() => isReadOnly(obj.Data.Details);
        [TestMethod] public void ProductKindTest() => isReadOnly(obj.Data.ProductKind);
        [TestMethod] public void BaseTypeIdTest() => isReadOnly(obj.Data.BaseTypeId);

        [TestMethod]
        public async Task FeaturesTest() {
            await testListAsync<FeatureType, FeatureTypeData, IFeatureTypesRepo>(
                x => x.ProductTypeId = obj.Id, d => new FeatureType(d));
        }

        [TestMethod]
        public async Task BaseTypeTest() {
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                obj.BaseTypeId, () => obj.BaseType.Data, ProductTypeFactory.Create);
        }

        [TestMethod] public async Task RelatedPricesTest() {

            await testListAsync<IPrice, PriceData, IPricesRepo>(
                x => {
                    x.ProductTypeId = obj.Id;
                    x.Kind = PriceKind.Possible;
                }, PriceFactory.Create);
        }

        [TestMethod] public async Task PossiblePricesTest() {
            isReadOnly();
            await RelatedPricesTest();
            Assert.AreEqual(obj.PossiblePrices.Count, obj.RelatedPrices.Count);
            Assert.IsInstanceOfType(obj.PossiblePrices, typeof(IReadOnlyList<PossiblePrice>));
        }

        [TestMethod] public async Task WhereConsumerTest() {
            await testListAsync<ProductRelationshipType, ProductRelationshipTypeData, IProductRelationshipTypesRepo>(
                x => x.ConsumerTypeId = obj.Id, d => new ProductRelationshipType(d));

        }

        [TestMethod] public async Task WhereProviderTest() {
            await testListAsync<ProductRelationshipType, ProductRelationshipTypeData, IProductRelationshipTypesRepo>(
                x => x.ProviderTypeId = obj.Id, d => new ProductRelationshipType(d));
        }

        [TestMethod]
        public async Task RelationsTest() {
            isReadOnly();
            await WhereProviderTest();
            await WhereConsumerTest();
            Assert.AreEqual(obj.WhereProvider.Count + obj.WhereConsumer.Count, obj.Relations.Count);
            foreach (var e in obj.WhereProvider) {
                var o = obj.Relations.First(x => x.Id == e.Id);
                allPropertiesAreEqual(o.Data, e.Data);
            }
            foreach (var e in obj.WhereConsumer) {
                var o = obj.Relations.First(x => x.Id == e.Id);
                allPropertiesAreEqual(o.Data, e.Data);
            }
        }

        [TestMethod] public void ConsumerProductsTest() =>

            testRelatedList<IProductType, ProductTypeData, ProductRelationshipType, IProductTypesRepo>(
                () => obj.ConsumerProducts,
                () => obj.WhereConsumer,
                (d, e) => {
                    d.Id = e.ProviderTypeId;
                    return ProductTypeFactory.Create(d);
                }, WhereConsumerTest, (x, r) => x.Id == r.ProviderTypeId);
        [TestMethod] public void ProviderProductsTest() =>
            testRelatedList<IProductType, ProductTypeData, ProductRelationshipType, IProductTypesRepo>(
                () => obj.ProviderProducts,
                () => obj.WhereProvider,
                (d, e) => {
                    d.Id = e.ConsumerTypeId;
                    return ProductTypeFactory.Create(d);
                }, WhereProviderTest, (x, r) => x.Id == r.ConsumerTypeId);
        
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