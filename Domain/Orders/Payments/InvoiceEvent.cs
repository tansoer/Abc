using Abc.Data.Orders;

namespace Abc.Domain.Orders.Payments {
    public sealed class InvoiceEvent :PaymentEvent {
        public InvoiceEvent() : this(null) { }
        public InvoiceEvent(OrderEventData d) : base(d) { }
        internal string invoiceId => get(Data?.InvoiceId);
        public Invoice Invoice => item<IInvoicesRepo, Invoice>(invoiceId);
    }

}
