using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Domain.Currencies;
using Abc.Domain.Orders.Lines;

namespace Abc.Domain.Orders.Payments {
    public interface IPaymentEvent :IOrderEvent {
        public OrderPayment Payment { get; }
        public IOrderLine OrderLine { get; }
    }

    public abstract class PaymentEvent :OrderEvent, IPaymentEvent {
        internal protected static string paymentEventId => nameOf<PaymentData>(x => x.OrderEventId);
        protected PaymentEvent() : this(null) { }
        protected PaymentEvent(OrderEventData d) : base(d) { }
        protected internal string orderLineId => get(Data?.OrderLineId);
        protected internal string paymentId => get(Data?.PaymentId);
        public OrderPayment Payment => item<IPaymentsRepo, BasePayment>(paymentId) as OrderPayment;
        public IOrderLine OrderLine => item<IOrderLinesRepo, IOrderLine>(orderLineId);
    }
}
