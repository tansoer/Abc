using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Common;

namespace Abc.Facade.Orders
{
    public sealed class OrderLineViewFactory
        :AbstractViewFactory<OrderLineData, IOrderLine, OrderLineView> {
        protected internal override IOrderLine toObject(OrderLineData d) =>
            OrderLineFactory.Create(d);

    }
}