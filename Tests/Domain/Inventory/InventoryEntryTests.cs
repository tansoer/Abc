using Abc.Data.Inventory;
using Abc.Data.Products;
using Abc.Domain.Inventory;
using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Inventory {
    [TestClass]
    public class InventoryEntryTests: AbstractTests<InventoryEntry, InventoryItem<InventoryEntryData>> {
        protected override InventoryEntry createObject() => InventoryEntryFactory.Create(random<InventoryEntryData>());
        [TestMethod] public void ProductTypeIdTest() => isReadOnly(obj.Data.ProductTypeId);
        [TestMethod] public async Task ProductTypeTest() {
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                obj.ProductTypeId,
                () => obj.ProductType.Data,
                d => ProductTypeFactory.Create(d));
            obj = createObject();
            isNotNull(obj.ProductType);
            isNotNull(obj.ProductType.Data);
        }
            
    }
}
