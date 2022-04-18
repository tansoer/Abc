using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Packets {

    [TestClass] public class ProductSetContentTests :SealedTests<ProductSetContent, Entity<ProductSetContentData>> {
        protected override ProductSetContent createObject()
            => new(GetRandom.ObjectOf<ProductSetContentData>());

        [TestMethod] public void ProductSetIdTest() => isReadOnly(obj.Data.ProductSetId);

        [TestMethod] public void ProductTypeIdTest() => isReadOnly(obj.Data.ProductTypeId);

        [TestMethod] public async Task ProductSetTest() =>
            await testItemAsync<ProductSetData, ProductSet, IProductSetsRepo>(
                obj.ProductSetId, () => obj.ProductSet.Data, d => new ProductSet(d));

        [TestMethod] public async Task ProductTypeTest() =>
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                obj.ProductTypeId, () => obj.ProductType.Data, ProductTypeFactory.Create);
    }
}