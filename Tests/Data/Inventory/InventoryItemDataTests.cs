using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Data.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Inventory {
    [TestClass]
    public class InventoryItemDataTests :AbstractTests<InventoryItemData, EntityBaseData> {
        private class testClass :InventoryItemData { }
        protected override InventoryItemData createObject() => GetRandom.ObjectOf<testClass>();
        [TestMethod] public void InventoryIdTest() => isNullable<string>();
    }
}