using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {

    [TestClass]
    public class ProductRelationshipTypeTests : SealedTests<ProductRelationshipType, RelationshipType<ProductRelationshipTypeData>> {
        protected override ProductRelationshipType createObject() 
            => new (GetRandom.ObjectOf<ProductRelationshipTypeData>());

        [TestMethod] public async Task ConsumerTest() {
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                obj.ConsumerTypeId, () => obj.Consumer.Data, ProductTypeFactory.Create);

        }

        [TestMethod]
        public async Task ProviderTest() => await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
            obj.ProviderTypeId, () => obj.Provider.Data, ProductTypeFactory.Create);
    }
}