using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {

    public sealed class OrderLineItemsRepo :PagedRepo<IOrderLineItem, OrderLineItemData>,
            IOrderLineItemsRepo {
        public OrderLineItemsRepo(OrderDb c = null) : base(c, c?.OrderLineItems) { }

        protected internal override IOrderLineItem toDomainObject(OrderLineItemData d)
            => OrderLineItemFactory.Create(d);
    }
}