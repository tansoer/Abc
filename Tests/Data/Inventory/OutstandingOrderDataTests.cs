using Abc.Data.Common;
using Abc.Data.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Inventory {
    [TestClass] public class OutstandingOrderDataTests :SealedTests<OutstandingOrderData, EntityBaseData> {
        [TestMethod] public void PurchaseOrderIdTest() => isNullable<string>();
        [TestMethod] public void InventoryEntryIdTest() => isNullable<string>();
    }
}
