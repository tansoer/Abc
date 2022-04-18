using Abc.Data.Products;
using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {

    [TestClass]
    public class ProductErrorTests : SealedTests<ProductError, BaseProduct<IProductType>> {

        [TestMethod] public async Task TypeTest() {
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                obj.TypeId, () => obj.Type.Data, ProductTypeFactory.Create);
        }
    }

}