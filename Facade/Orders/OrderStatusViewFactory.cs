using Abc.Data.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Common;

namespace Abc.Facade.Orders
{
    public sealed class OrderStatusViewFactory
        :AbstractViewFactory<OrderStatusData, IOrderStatus, OrderStatusView> {
        protected internal override IOrderStatus toObject(OrderStatusData d) =>
            OrderStatusFactory.Create(d);

    }
}