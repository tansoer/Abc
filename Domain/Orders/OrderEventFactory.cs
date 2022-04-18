using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Parties;
using Abc.Domain.Orders.Payments;
using Abc.Domain.Orders.Statuses;
using Abc.Domain.Orders.Terms;

namespace Abc.Domain.Orders {
    public static class OrderEventFactory {
        public static OrderEvent Create(OrderEventData d) =>
            d?.OrderEventType switch {
                OrderEventType.OpenEvent => new OpenOrderEvent(d),
                OrderEventType.CloseEvent => new CloseOrderEvent(d),
                OrderEventType.CancelEvent => new CancelOrderEvent(d),
                OrderEventType.AmendOrderLineEvent => new AmendOrderLineEvent(d),
                OrderEventType.AmendTermsAndConditionsEvent => new AmendTermsAndConditionsEvent(d),
                OrderEventType.AmendPartySummaryEvent => new AmendPartySummaryEvent(d),
                OrderEventType.DiscountEvent => new DiscountEvent(d),
                OrderEventType.DispatchEvent => new DispatchEvent(d),
                OrderEventType.ReceiptEvent => new ReceiptEvent(d),
                OrderEventType.InvoiceEvent => new InvoiceEvent(d),
                OrderEventType.MakePaymentEvent => new MakePaymentEvent(d),
                OrderEventType.AcceptPaymentEvent => new AcceptPaymentEvent(d),
                OrderEventType.MakeRefundEvent => new MakeRefundEvent(d),
                OrderEventType.AcceptRefundEvent => new AcceptRefundEvent(d),
                _ => new UnspecifiedOrderEvent(d)
            };
    }
}
