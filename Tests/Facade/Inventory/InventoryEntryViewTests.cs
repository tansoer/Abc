using Abc.Data.Products;
using Abc.Facade.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Inventory {
    [TestClass]
    public class InventoryEntryViewTests :SealedTests<InventoryEntryView, InventoryItemView> {
        [TestMethod] public void RestockPolicyIdTest() => isNullableProperty<string>("Restock Policy");
        [TestMethod] public void ProductTypeIdTest() => isNullableProperty<string>("Product Type");
        [TestMethod] public void InventoryEntryTypeTest() => isProperty<ProductKind>("Inventory Entry Type");
        [TestMethod] public void CapacityManagerIdTest() => isNullableProperty<string>("Capacity Manager");
    }
}
