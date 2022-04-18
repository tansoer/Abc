using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {

    [TestClass] public class ProductTests :SealedTests<Product, BaseProduct<ProductType>> {

        [TestMethod] public async Task TypeTest() {
            isReadOnly();
            var d = GetRandom.ObjectOf<ProductTypeData>();
            d.ProductKind = ProductKind.Product;
            d.Id = obj.TypeId;
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                d, () => obj.Type.Data, ProductTypeFactory.Create);
        }
    }
}