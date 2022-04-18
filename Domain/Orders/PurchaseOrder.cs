using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Payments;
using Abc.Domain.Parties.Signatures;
using System;
using System.Collections.Generic;

namespace Abc.Domain.Orders {
    public sealed class PurchaseOrder :Order {
        public PurchaseOrder() : this(null) { }
        public PurchaseOrder(OrderData d) : base(d) { }

        public DateTime DateSent => get(Data?.DateSentReceived);

        public override bool IsClosable() => LinesManager.IsClosable();
        public override bool IsCancellable() => LinesManager.IsCancellable();

        public IOrderEvent MakePayment(OrderPayment p, PartySignature s, Invoice i = null) {
            var d = createEventData(p, s, i);
            var errors = new List<string>();
            validatePayment(p, ref errors);
            validateSignature(s, ref errors);
            validateInvoice(i, ref errors);
            if (hasErrors(errors)) return reportErrors(d, errors);
            return MakePayment(d);
        }

        private IOrderEvent MakePayment(OrderEventData d) {
            throw new NotImplementedException();
        }
        internal static bool hasErrors(List<string> errors) => errors?.Count > 0;

        internal IOrderEvent reportErrors(OrderEventData d, List<string> errors) {
            d ??= new OrderEventData();
            d.OrderEventType = OrderEventType.Unspecified;
            d.Details = string.Join(", ", errors);
            d.Name = "Make payment";
            d.Code = "Error";
            d.OrderId = Id;
            d.ValidFrom = DateTime.Now;
            var e = OrderEventFactory.Create(d);
            add<IOrderEventsRepo, IOrderEvent>(e);
            return e;
        }

        private void validateInvoice(Invoice i, ref List<string> errors) {
            throw new NotImplementedException();
        }

        private void validateSignature(PartySignature s, ref List<string> errors) {
            throw new NotImplementedException();
        }

        private void validatePayment(OrderPayment p, ref List<string> errors) {
            throw new NotImplementedException();
        }

        private OrderEventData createEventData(OrderPayment p, PartySignature s, Invoice i) {
            throw new NotImplementedException();
        }

        protected internal override bool acceptRefund(AcceptRefundEvent e) => PaymentsManager.AcceptRefund(e);
        protected internal override bool makePayment(MakePaymentEvent e) => PaymentsManager.MakePayment(e);
        protected internal override bool processInvoice(InvoiceEvent e) => PaymentsManager.ReceiveInvoice(e);
        protected internal override bool processReceipt(IReceiptEvent e) => DeliveryManager.AcceptReceipt(e);
        protected internal override bool processDispatch(IDispatchEvent e) => DeliveryManager.ReturnItems(e);
    }
}
