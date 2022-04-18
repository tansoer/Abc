using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using System.Collections.Generic;

namespace Abc.Domain.Orders.Statuses {
    public sealed class CancelOrderEvent :LifecycleEvent, ICancelOrderEvent {
        internal string orderEventId = nameOf<ReturnedItem>(x => x.OrderEventId);
        public CancelOrderEvent() : this(null) { }
        public CancelOrderEvent(OrderEventData d) : base(d) { }
        public IReadOnlyList<ReturnedItem> ReturnedItems
            => list<IReturnedItemsRepo, ReturnedItem>(orderEventId, Id);
    }

    public interface ICancelOrderEvent :ILifecycleEvent {
    }
}
