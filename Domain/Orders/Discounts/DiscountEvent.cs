using Abc.Data.Orders;
using System.Collections.Generic;

namespace Abc.Domain.Orders.Discounts {
    public interface IDiscountEvent :IOrderEvent { }
    public sealed class DiscountEvent :OrderEvent, IDiscountEvent {
        internal static string discountEventId
            => nameOf<DiscountData>(x => x.OrderEventId);
        public DiscountEvent() : this(null) { }
        public DiscountEvent(OrderEventData d) : base(d) { }
        public IReadOnlyList<IDiscount> Discounts
            => list<IDiscountsRepo, IDiscount>(discountEventId, Id);
    }

}
