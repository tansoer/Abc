using Abc.Data.Parties;

namespace Abc.Domain.Orders.Parties {
    public sealed class OrderLineReceiver :DeliveryReceiver {
        public OrderLineReceiver() : this(null) { }
        public OrderLineReceiver(PartySummaryData d) : base(d) { }
    }
}