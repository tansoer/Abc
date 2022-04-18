using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Common;

namespace Abc.Facade.Orders
{
    public sealed class OrderEventViewFactory 
        :AbstractViewFactory<OrderEventData, IOrderEvent, OrderEventView> {
        protected internal override IOrderEvent toObject(OrderEventData d) =>
            OrderEventFactory.Create(d);

    }
}