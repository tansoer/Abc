using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {

    public sealed class OrderEventsRepo :PagedRepo<IOrderEvent, OrderEventData>,
        IOrderEventsRepo {

        public OrderEventsRepo(OrderDb c = null) : base(c, c?.OrderEvents) { }

        protected internal override IOrderEvent toDomainObject(OrderEventData d) => OrderEventFactory.Create(d);
    }
}