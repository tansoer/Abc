using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {
    [TestClass] public class ProductRelationshipTests
        :SealedTests<ProductRelationship, Relationship<ProductRelationshipData>> {
        protected override ProductRelationship createObject()
            => new(GetRandom.ObjectOf<ProductRelationshipData>());

        [TestMethod] public async Task TypeTest() {
            await testItemAsync<ProductRelationshipTypeData, ProductRelationshipType, IProductRelationshipTypesRepo>(
                obj.RelationshipTypeId, () => obj.Type.Data, d => new ProductRelationshipType(d));
        }

        [TestMethod] public async Task ConsumerTest() {
            await testItemAsync<ProductData, IProduct, IProductsRepo>(
                obj.ConsumerEntityId, () => obj.Consumer.Data, ProductFactory.Create);
        }

        [TestMethod] public async Task ProviderTest() => await testItemAsync<ProductData, IProduct, IProductsRepo>(
            obj.ProviderEntityId, () => obj.Provider.Data, ProductFactory.Create);
    }
}
