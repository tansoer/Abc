using Abc.Data.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Inventory {
    [TestClass] public class ReservationDataTests :SealedTests<ReservationData, InventoryItemData> {
        [TestMethod] public void ReservationRequestIdTest() => isNullable<string>();
        [TestMethod] public void CancellationPolicyRuleSetIdTest() => isNullable<string>();
    }
}
