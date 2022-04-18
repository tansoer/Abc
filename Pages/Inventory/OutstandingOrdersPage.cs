using Abc.Data.Inventory;
using Abc.Data.Orders;
using Abc.Domain.Inventory;
using Abc.Domain.Orders;
using Abc.Facade.Inventory;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Inventory {
    public sealed class OutstandingOrdersPage :ViewFactoryPage<OutstandingOrdersPage, IOutstandingOrdersRepo,
        OutstandingOrder, OutstandingOrderView, OutstandingOrderData, OutstandingOrderViewFactory> {
        protected override string title => InventoryTitles.OutstandingOrders;
        public OutstandingOrdersPage(IOutstandingOrdersRepo r) :base(r) {}
        private IEnumerable<SelectListItem> purchaseOrders;
        public IEnumerable<SelectListItem> PurchaseOrders => purchaseOrders ??= selectListByName<IOrdersRepo, IOrder, OrderData>(x => x.Data.OrderType == OrderType.PurchaseOrder);
        private IEnumerable<SelectListItem> inventoryEntries;
        public IEnumerable<SelectListItem> InventoryEntries => inventoryEntries ??= selectListByName<IInventoryEntriesRepo, InventoryEntry, InventoryEntryData>();
        protected override void tableColumns() {
            tableColumn(x => Item.InventoryEntryId, () => InventoryEntryName(Item.InventoryEntryId));
            tableColumn(x => Item.PurchaseOrderId, () => PurchaseOrderName(Item.PurchaseOrderId));
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.InventoryEntryId, () => InventoryEntryName(Item.InventoryEntryId));
            valueViewer(x => Item.PurchaseOrderId, () => PurchaseOrderName(Item.PurchaseOrderId));
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.InventoryEntryId, () => InventoryEntries);
            valueEditor(x => Item.PurchaseOrderId, () => PurchaseOrders);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => InventoryUrls.OutstandingOrders;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => Item = new();
        public string InventoryEntryName(string id) => itemName(InventoryEntries, id);
        public string PurchaseOrderName(string id) => itemName(PurchaseOrders, id);
    }
}
