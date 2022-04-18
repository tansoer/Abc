using Abc.Data.Orders;

namespace Abc.Domain.Orders {
    public static class OrderFactory {
        public static IOrder Create(OrderData d) => d?.OrderType switch {
            OrderType.PurchaseOrder => new PurchaseOrder(d),
            OrderType.SalesOrder => new SalesOrder(d),
            _ => new UnspecifiedOrder(d)
        };
    }
}