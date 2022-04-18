using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {

    public sealed class OrdersRepo :PagedRepo<IOrder, OrderData>, IOrdersRepo {
        public OrdersRepo(OrderDb c = null) : base(c, c?.Orders) { }
        protected internal override IOrder toDomainObject(OrderData d)
            => OrderFactory.Create(d);
    }
}