using Abc.Data.Inventory;
using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Orders;
using Abc.Facade.Inventory;
using Abc.Infra.Orders;
using Abc.Pages.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Inventory.Pages.OutstandingOrders {
    public abstract class OutstandingOrdersTests :BaseInventoryTests<OutstandingOrderView, OutstandingOrderData> {
        private OrderDb orderDb;
        protected List<SelectListItem> inventoryEntries => createSelectList(db.InventoryEntries);
        protected List<SelectListItem> purchaseOrders => createSelectList(orderDb.Orders, x => x.OrderType == OrderType.PurchaseOrder);
        protected override OutstandingOrderView toView(OutstandingOrderData d)
            => new OutstandingOrderViewFactory().Create(new OutstandingOrder(d));
        [TestInitialize] public override void TestInitialize() {
            orderDb = getContext<OrderDb>();
            base.TestInitialize();
        }
        protected override string baseUrl() => InventoryUrls.OutstandingOrders;
        protected override void changeValuesExceptId(OutstandingOrderData d) {
            var id = d.Id;
            d = random<OutstandingOrderData>();
            d.Id = id;
        }
        protected override string getItemId(OutstandingOrderData d) => d.Id;
        protected override void setItemId(OutstandingOrderData d, string id) => d.Id = id;
        protected override void addRelatedItems(OutstandingOrderData d) {
            if (d is null) return;
            addInventoryEntry(d.InventoryEntryId);
            addPurchaseOrder(d.PurchaseOrderId);
        }
        private void addPurchaseOrder(string id) {
            var d = random<OrderData>();
            d.Id = id;
            d.OrderType = OrderType.PurchaseOrder;
            add<IOrdersRepo, IOrder>(OrderFactory.Create(d));
        }
        protected override IEnumerable<Expression<Func<OutstandingOrderView, object>>> indexPageColumns =>
            new Expression<Func<OutstandingOrderView, object>>[] {
                x => x.InventoryEntryId,
                x => x.PurchaseOrderId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.InventoryEntryId, Unspecified.String);
            canView(item, m => m.PurchaseOrderId, Unspecified.String);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canSelect(item, m => m.InventoryEntryId, inventoryEntries);
            canSelect(item, m => m.PurchaseOrderId, purchaseOrders);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canSelect(null, m => m.InventoryEntryId, inventoryEntries);
            canSelect(null, m => m.PurchaseOrderId, purchaseOrders);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :OutstandingOrdersTests { }
    [TestClass] public class DeletePageTests :OutstandingOrdersTests { }
    [TestClass] public class DetailsPageTests :OutstandingOrdersTests { }
    [TestClass] public class EditPageTests :OutstandingOrdersTests { }
    [TestClass] public class IndexPageTests :OutstandingOrdersTests { }
}
