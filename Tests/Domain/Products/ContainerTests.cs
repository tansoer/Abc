using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {

    [TestClass]
    public class ContainerTests : SealedTests<Container, BaseProduct<ContainerType>> {

        [TestMethod]
        public async Task TypeTest() {
            isReadOnly();
            var d = GetRandom.ObjectOf<ProductTypeData>();
            d.ProductKind = ProductKind.Container;
            d.Id = obj.TypeId;
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                d, () => obj.Type.Data, ProductTypeFactory.Create);
        }

        [TestMethod] public async Task ItemsTest() {
            await testListAsync<ContainerItem, ContainerItemData, IContainerItemsRepo>(
                x => x.ContainerId = obj.Id, d => new ContainerItem(d));
        }
    }

}