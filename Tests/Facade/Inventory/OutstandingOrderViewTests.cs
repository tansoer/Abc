using Abc.Facade.Common;
using Abc.Facade.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Inventory {
    [TestClass]
    public class OutstandingOrderViewTests :SealedTests<OutstandingOrderView, EntityBaseView> {
        [TestMethod] public void PurchaseOrderIdTest() => isNullableProperty<string>("Purchase Order");
        [TestMethod] public void InventoryEntryIdTest() => isNullableProperty<string>("Inventory Entry");
    }
}
