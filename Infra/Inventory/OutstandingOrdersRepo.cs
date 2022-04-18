using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;

namespace Abc.Infra.Inventory {
    public sealed class OutstandingOrdersRepo:
        PagedRepo<OutstandingOrder, OutstandingOrderData>, IOutstandingOrdersRepo{
        public OutstandingOrdersRepo(InventoryDb c = null) : base(c, c?.OutstandingOrders) { }
        protected internal override OutstandingOrder toDomainObject(OutstandingOrderData d)
            => new OutstandingOrder(d);
    }
}