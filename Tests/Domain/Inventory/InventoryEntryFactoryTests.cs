
using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Inventory {
    [TestClass]
    public class InventoryEntryFactoryTests: TestsBase {
        private InventoryEntryData d;
        [TestInitialize]
        public void TestInitialize() {
            type = typeof(InventoryEntryFactory);
            d = random<InventoryEntryData>();
        }
        [TestMethod] public void CreateTest() {
            createProductTest();
            createServiceTest();
        }
        private void createProductTest() {
            d.InventoryEntryType = Abc.Data.Products.ProductKind.Service;
            var o = InventoryEntryFactory.Create(d);
            isInstanceOfType<ServiceInventoryEntry>(o);
            allPropertiesAreEqual(d, o.Data);
        }
        private void createServiceTest() {
            while (d.InventoryEntryType == Abc.Data.Products.ProductKind.Service) {
                d = random<InventoryEntryData>();
            }
            var o = InventoryEntryFactory.Create(d);
            isInstanceOfType<ProductInventoryEntry>(o);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}
