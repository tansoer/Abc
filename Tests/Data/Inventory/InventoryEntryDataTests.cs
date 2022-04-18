using Abc.Data.Inventory;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Inventory {
    [TestClass] public class InventoryEntryDataTests :SealedTests<InventoryEntryData, InventoryItemData> {
        [TestMethod] public void RestockPolicyIdTest() => isNullable<string>();
        [TestMethod] public void ProductTypeIdTest() => isNullable<string>();
        [TestMethod] public void CapacityManagerIdTest() => isNullable<string>();
        [TestMethod] public void InventoryEntryTypeTest() => isProperty<ProductKind>();
    }
}
