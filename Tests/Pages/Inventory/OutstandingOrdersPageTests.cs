using Abc.Aids.Random;
using Abc.Data.Inventory;
using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Orders;
using Abc.Facade.Inventory;
using Abc.Pages.Inventory;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Inventory {
    [TestClass]
    public class OutstandingOrdersPageTests :SealedViewFactoryPageTests<OutstandingOrdersPage, 
        IOutstandingOrdersRepo, OutstandingOrder, OutstandingOrderView, OutstandingOrderData,
        OutstandingOrderViewFactory> {
        protected override string pageTitle => InventoryTitles.OutstandingOrders;
        protected override string pageUrl => InventoryUrls.OutstandingOrders;
        protected override OutstandingOrder toObject(OutstandingOrderData d) => new(d);

        private OutstandingOrderData data;
        private InventoryEntryData inventoryEntryData;
        private OrderData orderData;

        private class repo :mockRepo<OutstandingOrder, OutstandingOrderData>, IOutstandingOrdersRepo {}
        private class inventoryEntriesRepo :mockRepo<InventoryEntry, InventoryEntryData>, IInventoryEntriesRepo {}
        private class ordersRepo :mockRepo<IOrder, OrderData>, IOrdersRepo {}

        private repo outstandingOrders;
        private inventoryEntriesRepo inventoryEntries;
        private ordersRepo orders;

        protected override OutstandingOrdersPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new OutstandingOrdersPage(outstandingOrders);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(outstandingOrders, inventoryEntries, orders);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void initializeRepos() {
            outstandingOrders = new();
            inventoryEntries = new();
            orders = new();
        }
        private void setRandomData() {
            data = random<OutstandingOrderData>();
            inventoryEntryData = random<InventoryEntryData>();
            orderData = random<OrderData>();
            orderData.OrderType = OrderType.PurchaseOrder;
        }
        private void addRandomDataToRepos() {
            addRandomOutstandingOrders();
            addRandomInventoryEntries();
            addRandomPurchaseOrders();
        }
        private void addRandomOutstandingOrders() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : random<OutstandingOrderData>();
                outstandingOrders.Add(new(d));
            }
        }
        private void addRandomInventoryEntries() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? inventoryEntryData : random<InventoryEntryData>();
                inventoryEntries.Add(InventoryEntryFactory.Create(d));
            }
        }
        private void addRandomPurchaseOrders() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? orderData : random<OrderData>();
                d.OrderType = OrderType.PurchaseOrder;
                orders.Add(OrderFactory.Create(d));
            }
        }

        [TestMethod]
        public void InventoryEntriesTest() {
            var list = inventoryEntries.Get();
            areEqual(list.Count + 1, obj.InventoryEntries.Count());
        }

        [TestMethod]
        public void PurchaseOrdersTest() {
            var list = orders.Get();
            areEqual(list.Count + 1, obj.PurchaseOrders.Count());
        }

        [TestMethod]
        public void InventoryEntryNameTest() {
            var name = obj.InventoryEntryName(inventoryEntryData.Id);
            areEqual(inventoryEntryData.Name, name);
        }

        [TestMethod]
        public void PurchaseOrderNameTest() {
            var name = obj.PurchaseOrderName(orderData.Id);
            areEqual(orderData.Name, name);
        }
    }
}
