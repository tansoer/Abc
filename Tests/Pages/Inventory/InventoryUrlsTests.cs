using Abc.Pages.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Inventory {
    [TestClass]
    public class InventoryUrlsTests :TestsBase{
        [TestInitialize] public void TestInitialize() => type = typeof(InventoryUrls);
        [TestMethod] public void InventoriesTest() => areEqual("/Inventory/Inventories", InventoryUrls.Inventories);
        [TestMethod] public void InventoryEntriesTest() => areEqual("/Inventory/InventoryEntries", InventoryUrls.InventoryEntries);
        [TestMethod] public void OutstandingOrdersTest() => areEqual("/Inventory/OutstandingOrders", InventoryUrls.OutstandingOrders);
        [TestMethod] public void ReservationsTest() => areEqual("/Inventory/Reservations", InventoryUrls.Reservations);
        [TestMethod] public void ReservationReceiversTest() => areEqual("/Inventory/ReservationReceivers", InventoryUrls.ReservationReceivers);
        [TestMethod] public void ReservationRequestsTest() => areEqual("/Inventory/ReservationRequests", InventoryUrls.ReservationRequests);
        [TestMethod] public void RestockPoliciesTest() => areEqual("/Inventory/RestockPolicies", InventoryUrls.RestockPolicies);
        [TestMethod] public void CapacityManagersTest() => areEqual("/Inventory/CapacityManagers", InventoryUrls.CapacityManagers);
    }
}
