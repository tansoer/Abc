using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Domain.Currencies;
using System.Collections.Generic;

namespace Abc.Domain.Orders.Payments {
    public interface IOrderPaymentsManager :IOrderManager {
        IReadOnlyList<OrderPayment> Payments { get;}
        IReadOnlyList<Invoice> Invoices { get;}
        bool AcceptPayment(AcceptPaymentEvent acceptPaymentEvent);
        bool MakeRefund(MakeRefundEvent makeRefundEvent);
        bool SendInvoice(InvoiceEvent invoiceEvent);
        bool AcceptRefund(AcceptRefundEvent acceptRefundEvent);
        bool MakePayment(MakePaymentEvent makePaymentEvent);
        bool ReceiveInvoice(InvoiceEvent invoiceEvent);
    }
    public sealed class OrderPaymentsManager :OrderManager, IOrderPaymentsManager {
        internal static string orderIdInPayment => nameOf<PaymentData>(x => x.OrderId);
        internal static string orderIdInInvoice => nameOf<InvoiceData>(x => x.OrderId);

        public OrderPaymentsManager(IOrder o) : base(o) { }
        public OrderPaymentsManager() { }

        internal IReadOnlyList<BasePayment> payments
            => list<IPaymentsRepo, BasePayment>(orderIdInPayment, order?.Id);
        public IReadOnlyList<OrderPayment> Payments
            => list<OrderPayment, BasePayment>(payments);
        public IReadOnlyList<Invoice> Invoices
            => list<IInvoicesRepo, Invoice>(orderIdInInvoice, order?.Id);
        public bool AcceptPayment(AcceptPaymentEvent e) => throw new System.NotImplementedException();
        public bool MakeRefund(MakeRefundEvent e) => throw new System.NotImplementedException();
        public bool SendInvoice(InvoiceEvent e) => throw new System.NotImplementedException();
        public bool AcceptRefund(AcceptRefundEvent e) => throw new System.NotImplementedException();
        public bool MakePayment(MakePaymentEvent e) => throw new System.NotImplementedException();
        public bool ReceiveInvoice(InvoiceEvent e) => throw new System.NotImplementedException();
    }
}
