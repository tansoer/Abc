using Abc.Facade.Common;
using Abc.Facade.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Inventory {
    [TestClass]
    public class InventoryItemViewTests :AbstractTests<InventoryItemView, EntityBaseView> {
        private class testClass :InventoryItemView { }
        protected override InventoryItemView createObject() => random<testClass>();

        [TestMethod] public void InventoryIdTest() => isNullableProperty<string>("Inventory");
    }
}
