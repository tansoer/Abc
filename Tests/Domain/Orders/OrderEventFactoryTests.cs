using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Parties;
using Abc.Domain.Orders.Payments;
using Abc.Domain.Orders.Statuses;
using Abc.Domain.Orders.Terms;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Domain.Orders {

    [TestClass]
    public class OrderEventFactoryTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(OrderEventFactory);

        [DataTestMethod]
        [DataRow(OrderEventType.OpenEvent, typeof(OpenOrderEvent))]
        [DataRow(OrderEventType.CloseEvent, typeof(CloseOrderEvent))]
        [DataRow(OrderEventType.CancelEvent, typeof(CancelOrderEvent))]
        [DataRow(OrderEventType.AmendOrderLineEvent, typeof(AmendOrderLineEvent))]
        [DataRow(OrderEventType.AmendTermsAndConditionsEvent, typeof(AmendTermsAndConditionsEvent))]
        [DataRow(OrderEventType.AmendPartySummaryEvent, typeof(AmendPartySummaryEvent))]
        [DataRow(OrderEventType.DiscountEvent, typeof(DiscountEvent))]
        [DataRow(OrderEventType.DispatchEvent, typeof(DispatchEvent))]
        [DataRow(OrderEventType.ReceiptEvent, typeof(ReceiptEvent))]
        [DataRow(OrderEventType.InvoiceEvent, typeof(InvoiceEvent))]
        [DataRow(OrderEventType.MakePaymentEvent, typeof(MakePaymentEvent))]
        [DataRow(OrderEventType.AcceptPaymentEvent, typeof(AcceptPaymentEvent))]
        [DataRow(OrderEventType.MakeRefundEvent, typeof(MakeRefundEvent))]
        [DataRow(OrderEventType.AcceptRefundEvent, typeof(AcceptRefundEvent))]
        public void CreateTest(OrderEventType eventType, Type t) {
            var d = GetRandom.ObjectOf<OrderEventData>();
            d.OrderEventType = eventType;
            var o = OrderEventFactory.Create(d);
            Assert.IsInstanceOfType(o, t);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}