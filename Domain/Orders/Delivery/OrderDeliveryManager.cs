using System;

namespace Abc.Domain.Orders.Delivery {
    public interface IOrderDeliveryManager {
        bool ReceiptReturned(IReceiptEvent receiptEvent);
        bool DispatchItems(IDispatchEvent dispatchEvent);
        bool ReturnItems(IDispatchEvent dispatchEvent);
        bool AcceptReceipt(IReceiptEvent receiptEvent);
    }
    public sealed class OrderDeliveryManager: OrderManager, IOrderDeliveryManager {
        public OrderDeliveryManager(IOrder o) : base(o) { }
        public OrderDeliveryManager() { }
        public bool DispatchItems(IDispatchEvent e) => throw new NotImplementedException();
        public bool ReturnItems(IDispatchEvent e) => throw new NotImplementedException();
        public bool AcceptReceipt(IReceiptEvent e) => throw new NotImplementedException();
        public bool ReceiptReturned(IReceiptEvent e) => throw new NotImplementedException();
    }
}
