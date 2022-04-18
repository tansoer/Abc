using Abc.Data.Inventory;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Inventory {
    [TestClass]
    public class InventoryItemTests: 
        AbstractTests<InventoryItem<testDataClass>, Entity<testDataClass>> {
        protected override InventoryItem<testDataClass> createObject()
            => new testClass(random<testDataClass>());
            
        private class testClass: InventoryItem<testDataClass> {
            public testClass() { }
            public testClass(testDataClass d) : base(d) { }
        }
        [TestMethod] public void InventoryIdTest() => isReadOnly(obj.Data.InventoryId);
    }
    public class testDataClass :InventoryItemData { }
}
