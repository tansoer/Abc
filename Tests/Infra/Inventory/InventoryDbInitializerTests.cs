using Abc.Infra.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Inventory {
    [TestClass] public class InventoryDbInitializerTests :DbInitializerTests<InventoryDb> {
        public InventoryDbInitializerTests() {
            type = typeof(InventoryDbInitializer);
            db = new InventoryDb(options);
            InventoryDbInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() {}
        [TestMethod] public void InventoriesTest() => areEqual(0, db.Inventories.Count());
        [TestMethod] public void CapacityManagersTest() => areEqual(0, db.CapacityManagers.Count());
        [TestMethod] public void InventoryEntriesTest() => areEqual(0, db.InventoryEntries.Count());
        [TestMethod] public void OutstandingOrdersTest() => areEqual(0, db.OutstandingOrders.Count());
        [TestMethod] public void ReservationReceiversTest() => areEqual(0, db.ReservationReceivers.Count());
        [TestMethod] public void ReservationRequestsTest() => areEqual(0, db.ReservationRequests.Count());
        [TestMethod] public void ReservationsTest() => areEqual(0, db.Reservations.Count());
        [TestMethod] public void RestockPoliciesTest() => areEqual(0, db.RestockPolicies.Count());
    }
}