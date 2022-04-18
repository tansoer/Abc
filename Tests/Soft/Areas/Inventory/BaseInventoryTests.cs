using Abc.Data.Common;
using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Common;
using Abc.Infra.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Inventory {
    public abstract class BaseInventoryTests<TView, TData> :BaseAcceptanceTests<TView, TData, InventoryDb> 
        where TData : EntityBaseData where TView : EntityBaseView {

        protected override void doOnCreated(InventoryDb c) => clearAll(c);

        [TestCleanup]
        public override void TestCleanup() {
            base.TestCleanup();
            clearAll(db);
        }

        protected virtual void clearAll(InventoryDb c) {
            if (c is null) return;
            clear(c.Inventories);
            clear(c.InventoryEntries);
            clear(c.Reservations);
            clear(c.ReservationRequests);
            clear(c.RestockPolicies);
            clear(c.OutstandingOrders);
            clear(c.ReservationReceivers);
            clear(c.CapacityManagers);
        }

        protected void addInventory(string id) {
            var d = random<InventoryData>();
            d.Id = id;
            add<IInventoriesRepo, Abc.Domain.Inventory.Inventory>(new(d));
        }
        protected void addInventoryEntry(string id) {
            var d = random<InventoryEntryData>();
            d.Id = id;
            add<IInventoryEntriesRepo, InventoryEntry>(InventoryEntryFactory.Create(d));
        }
        protected void addReservation(string id) {
            var d = random<ReservationData>();
            d.Id = id;
            add<IReservationsRepo, Reservation>(new(d));
        }
        protected void addReservationRequest(string id) {
            var d = random<ReservationRequestData>();
            d.Id = id;
            add<IReservationRequestsRepo, ReservationRequest>(new(d));
        }
        protected void addRestockPolicy(string id) 
            => addRandomRecord<RestockPolicyData>(id);
        
        protected void addOutstandingOrder(string id) {
            var d = random<OutstandingOrderData>();
            d.Id = id;
            add<IOutstandingOrdersRepo, OutstandingOrder>(new(d));
        }
        protected void addReservationReceiver(string id) {
            var d = random<ReservationReceiverData>();
            d.Id = id;
            add<IReservationReceiversRepo, ReservationReceiver>(new(d));
        }
        protected void addCapacityManager(string id) =>
            addRandomRecord<CapacityManagerData>(id);
    }
}
