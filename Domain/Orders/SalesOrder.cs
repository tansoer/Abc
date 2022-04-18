using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Orders.Payments;
using System;
using System.Collections.Generic;

namespace Abc.Domain.Orders {
    public sealed class SalesOrder :Order {
        public SalesOrder() : this(null) { }
        public SalesOrder(OrderData d) : base(d) { }

        public IReadOnlyList<Discount> PossibleDiscounts => OrdersManager.GetDiscounts(this);
        public override bool IsClosable() => LinesManager.IsClosable();
        public override bool IsCancellable() => LinesManager.IsCancellable();
        public DateTime DatePurchaseOrderReceived => get(Data?.DateSentReceived);
        public string PurchaseOrderId => get(Data?.PurchaseOrderId);
        public PurchaseOrder PurchaseOrder => item<IOrdersRepo, IOrder>(PurchaseOrderId) as PurchaseOrder;
        protected internal override bool processReceipt(IReceiptEvent e) => DeliveryManager.ReceiptReturned(e);
        protected internal override bool processDispatch(IDispatchEvent e) => DeliveryManager.DispatchItems(e);
        protected internal override bool acceptPayment(AcceptPaymentEvent e) => PaymentsManager.AcceptPayment(e);
        protected internal override bool makeRefund(MakeRefundEvent e) => PaymentsManager.MakeRefund(e);
        protected internal override bool processInvoice(InvoiceEvent e) => PaymentsManager.SendInvoice(e);
    }
}
