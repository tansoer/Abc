using Abc.Data.Common;
using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Domain.Currencies;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Payments;
using Abc.Domain.Orders.Statuses;
using Abc.Domain.Orders.Terms;
using Abc.Facade.Common;
using Abc.Infra.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Orders {
    public abstract class BaseOrdersTests<TView, TData>
        :BaseAcceptanceTests<TView, TData, OrderDb> where TData : EntityBaseData where TView : EntityBaseView {
        protected override void doOnCreated(OrderDb c) => clearAll(c);

        [TestCleanup]
        public override void TestCleanup() {
            base.TestCleanup();
            clearAll(db);
        }

        protected void clearAll(OrderDb c) {
            if (c is null) return;
            clear(c.Invoices);
            clear(c.ProductDeliveries);
            clear(c.DeliveryTypes);
            clear(c.Discounts);
            clear(c.DiscountTypes);
            clear(c.Orders);
            clear(c.OrderEvents);
            clear(c.OrderLines);
            clear(c.OrderManagers);
            clear(c.OrderPayments);
            clear(c.OrderLineItems);
            clear(c.ReturnedItems);
            clear(c.SalesTaxPolicies);
            clear(c.TermsAndConditions);
            clear(c.OrderStatuses);
        }

        protected override void isCorrectContext() {
            var n = "Order";
            var contextName = db.GetType().Name;
            Assert.IsTrue(contextName.StartsWith(n), $"Not testing {n}");
        }

        protected void addInvoice(string id) {
            var d = random<InvoiceData>();
            d.Id = id;
            add<IInvoicesRepo, Invoice>(new(d));
        }

        protected void addProductDelivery(string id) {
            var d = random<ProductDeliveryData>();
            d.Id = id;
            add<IProductDeliveriesRepo, ProductDelivery>(new(d));
        }

        protected void addDeliveryType(string id) {
            var d = random<DeliveryTypeData>();
            d.Id = id;
            add<IDeliveryTypesRepo, DeliveryType>(new(d));
        }

        protected void addDiscount(string id) {
            var d = random<DiscountData>();
            d.Id = id;
            add<IDiscountsRepo, IDiscount>(DiscountFactory.Create(d));
        }

        protected void addDiscountType(string id) {
            var d = random<DiscountTypeData>();
            d.Id = id;
            add<IDiscountTypesRepo, IDiscountType>(new DiscountType(d));
        }

        protected void addOrder(string id) {
            var d = random<OrderData>();
            d.Id = id;
            add<IOrdersRepo, IOrder>(OrderFactory.Create(d));
        }

        protected void addOrderEvent(string id) {
            var d = random<OrderEventData>();
            d.Id = id;
            add<IOrderEventsRepo, IOrderEvent>(OrderEventFactory.Create(d));
        }

        protected void addOrderLine(string id) {
            var d = random<OrderLineData>();
            d.Id = id;
            add<IOrderLinesRepo, IOrderLine>(OrderLineFactory.Create(d));
        }

        protected void addOrderManager(string id) {
            var d = random<ManagerData>();
            d.Id = id;
            add<IOrdersManagersRepo, IOrdersManager>(new OrdersManager(d));
        }

        protected void addOrderPayment(string id) {
            var d = random<PaymentData>();
            d.Id = id;
            add<IPaymentsRepo, BasePayment>(PaymentFactory.Create(d));
        }

        protected void addOrderLineItem(string id) {
            var d = random<OrderLineItemData>();
            d.Id = id;
            add<IOrderLineItemsRepo, IOrderLineItem>(OrderLineItemFactory.Create(d));
        }

        protected void addReturnedItem(string id) {
            var d = random<ReturnedItemData>();
            d.Id = id;
            add<IReturnedItemsRepo, ReturnedItem>(new(d));
        }

        protected void addSalesTaxPolicy(string id) {
            var d = random<SalesTaxPolicyData>();
            d.Id = id;
            add<ISalesTaxPoliciesRepo, SalesTaxPolicy>(new(d));
        }

        protected void addTermsAndConditions(string id) {
            var d = random<TermsAndConditionsData>();
            d.Id = id;
            add<ITermsAndConditionsRepo, TermsAndConditions>(new(d));
        }

        protected void addOrderStatus(string id) {
            var d = random<OrderStatusData>();
            d.Id = id;
            add<IOrderStatusesRepo, IOrderStatus>(OrderStatusFactory.Create(d));
        }
    }
}
