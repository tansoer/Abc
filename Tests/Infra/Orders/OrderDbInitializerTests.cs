using Abc.Infra.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Orders {
    [TestClass] public class OrderDbInitializerTests :DbInitializerTests<OrderDb> {
        public OrderDbInitializerTests() {
            type = typeof(OrderDbInitializer);
            db = new OrderDb(options);
            OrderDbInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void DiscountTypesTest() => areEqual(0, db.DiscountTypes.Count());
        [TestMethod] public void DiscountsTest() => areEqual(0, db.Discounts.Count());
        [TestMethod] public void InvoicesTest() => areEqual(0, db.Invoices.Count());
        [TestMethod] public void OrderEventsTest() => areEqual(0, db.OrderEvents.Count());
        [TestMethod] public void OrderLinesTest() => areEqual(0, db.OrderLines.Count());
        [TestMethod] public void OrderLineItemsTest() => areEqual(0, db.OrderLineItems.Count());
        [TestMethod] public void OrderManagersTest() =>areEqual(0, db.OrderManagers.Count());
        [TestMethod] public void OrderPaymentsTest() => areEqual(0, db.OrderPayments.Count());
        [TestMethod] public void OrdersTest() => areEqual(0, db.Orders.Count());
        [TestMethod] public void RejectedItemsTest() => areEqual(0, db.OrderLineItems.Count());
        [TestMethod] public void ReturnedItemsTest() => areEqual(0, db.ReturnedItems.Count());
        [TestMethod] public void SalesTaxPoliciesTest() => areEqual(0, db.SalesTaxPolicies.Count());
        [TestMethod] public void TermsAndConditionsTest() => areEqual(0, db.TermsAndConditions.Count());
        [TestMethod] public void ProductDeliveriesTest() => areEqual(0, db.ProductDeliveries.Count());
        [TestMethod] public void DeliveryTypesTest() => areEqual(0, db.DeliveryTypes.Count());
        [TestMethod] public void OrderStatusesTest() => areEqual(0, db.OrderStatuses.Count());
    }
}