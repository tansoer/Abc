using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {

    [TestClass]
    public class UniqueProductTests : SealedTests<UniqueProduct, PoleomorphProduct<UniqueProductType>> {

        [TestMethod]
        public async Task TypeTest() {
            isReadOnly();
            var d = GetRandom.ObjectOf<ProductTypeData>();
            d.ProductKind = ProductKind.UniqueProduct;
            d.Id = obj.TypeId;
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                d, () => obj.Type.Data, ProductTypeFactory.Create);
        }

        [TestMethod]
        public async Task IdTest() {
            await TypeTest();
            isReadOnly(obj.Type.Id);
        }

    }

}