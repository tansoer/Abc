using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {

    public sealed class ReturnedItemsRepo :EntityRepo<ReturnedItem, ReturnedItemData>,
            IReturnedItemsRepo {
        public ReturnedItemsRepo(OrderDb c = null) : base(c, c?.ReturnedItems) { }
    }
}