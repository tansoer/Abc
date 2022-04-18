using Abc.Data.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {
    public sealed class OrderStatusesRepo :PagedRepo<IOrderStatus, OrderStatusData>,
        IOrderStatusesRepo {

        public OrderStatusesRepo(OrderDb c = null) : base(c, c?.OrderStatuses) { }

        protected internal override IOrderStatus toDomainObject(OrderStatusData d) => OrderStatusFactory.Create(d);
    }
}