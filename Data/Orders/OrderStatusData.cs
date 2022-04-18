using Abc.Data.Common;

namespace Abc.Data.Orders {
    public sealed class OrderStatusData: EntityBaseData {
        public string OrderId { get; set; }
        public string OrderEventId { get; set; }
        public OrderLifecycleStatus OrderStatus { get; set; }
    }
}
