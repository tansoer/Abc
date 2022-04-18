using Abc.Pages.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Inventory {
    [TestClass]
    public class InventoryTitlesTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(InventoryTitles);
        [TestMethod] public void InventoriesTest() => areEqual("Inventories", InventoryTitles.Inventories);
        [TestMethod] public void InventoryEntriesTest() => areEqual("Inventory Entries", InventoryTitles.InventoryEntries);
        [TestMethod] public void OutstandingOrdersTest() => areEqual("Outstanding Orders", InventoryTitles.OutstandingOrders);
        [TestMethod] public void ReservationsTest() => areEqual("Reservations", InventoryTitles.Reservations);
        [TestMethod] public void ReservationReceiversTest() => areEqual("Reservation Receivers", InventoryTitles.ReservationReceivers);
        [TestMethod] public void ReservationRequestsTest() => areEqual("Reservation Requests", InventoryTitles.ReservationRequests);
        [TestMethod] public void RestockPoliciesTest() => areEqual("Restock Policies", InventoryTitles.RestockPolicies);
        [TestMethod] public void CapacityManagersTest() => areEqual("Capacity Managers", InventoryTitles.CapacityManagers);
    }
}
