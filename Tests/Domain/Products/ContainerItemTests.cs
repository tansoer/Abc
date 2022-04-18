using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {
    [TestClass]
    public class ContainerItemTests :SealedTests<ContainerItem, Entity<ContainerItemData>> {
        protected override ContainerItem createObject() => new (GetRandom.ObjectOf<ContainerItemData>());
        [TestMethod] public void ContainerIdTest() => isReadOnly(obj.Data.ContainerId);
        [TestMethod] public void ProductIdTest() => isReadOnly(obj.Data.ProductId);

        [TestMethod] public async Task ProductTest() {
            await testItemAsync<ProductData, IProduct, IProductsRepo>(
                obj.ProductId, () => obj.Product.Data, ProductFactory.Create);
        }

        [TestMethod] public void RowNumberTest() => isReadOnly(obj.Data.RowNumber);
        [TestMethod] public void ColumnNumberTest() => isReadOnly(obj.Data.ColumnNumber);
        [TestMethod] public void LevelNumberTest() => isReadOnly(obj.Data.LevelNumber);
    }
}
