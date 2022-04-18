using Abc.Data.Inventory;
using Abc.Data.Products;
using Abc.Domain.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Inventory {
    [TestClass]
    public class ServiceInventoryEntryTests: SealedTests<ServiceInventoryEntry, InventoryEntry> {
        protected override ServiceInventoryEntry createObject() {
            var d = random<InventoryEntryData>();
            d.InventoryEntryType = ProductKind.Service;
            return InventoryEntryFactory.Create(d) as ServiceInventoryEntry;
        }
        [TestMethod] public void CapacityManagerIdTest() => isReadOnly(obj.Data.CapacityManagerId);
        [TestMethod] public async Task CapacityManagerTest() {
            await testItemAsync<CapacityManagerData, CapacityManager, ICapacityManagersRepo>(
                obj.CapacityManagerId,
                () => obj.CapacityManager.Data,
                d => new CapacityManager(d));
            obj = createObject();
            isNotNull(obj.CapacityManager);
            isNotNull(obj.CapacityManager.Data);
        }
    }    
}
