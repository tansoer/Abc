using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {
    public sealed class OrderLinesRepo :PagedRepo<IOrderLine, OrderLineData>, IOrderLinesRepo {
        public OrderLinesRepo(OrderDb c = null) : base(c, c?.OrderLines) { }
        protected internal override IOrderLine toDomainObject(OrderLineData d) => OrderLineFactory.Create(d);
    }
}