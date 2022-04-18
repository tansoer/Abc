using Abc.Data.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Inventory {
    [TestClass] public class ReservationRequestDataTests :SealedTests<ReservationRequestData, InventoryItemData> {
        [TestMethod] public void RequesterPartySummaryIdTest() => isNullable<string>();
        [TestMethod] public void NumberRequestedTest() => isProperty<uint>();
        [TestMethod] public void ProductTypeIdTest() => isNullable<string>();
        [TestMethod] public void RuleContextIdTest() => isNullable<string>();
        [TestMethod] public void RuleOverridesIdTest() => isNullable<string>();
    }
}
