using Abc.Data.Parties;

namespace Abc.Domain.Orders.Parties {
    public sealed class OrderReceiver :DeliveryReceiver {
        public OrderReceiver() : this(null) { }
        public OrderReceiver(PartySummaryData d) : base(d) { }
    }
}