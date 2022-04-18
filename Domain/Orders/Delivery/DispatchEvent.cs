using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using System.Collections.Generic;

namespace Abc.Domain.Orders.Delivery {
    public sealed class DispatchEvent :DeliveryEvent, IDispatchEvent {
        public DispatchEvent() : this(null) { }
        public DispatchEvent(OrderEventData d) : base(d) { }
        public IReadOnlyList<DispatchLine> DispatchLines => list<DispatchLine, IDeliveryOrderLine>(DeliveryLines);
    }
    public interface IDispatchEvent :IDeliveryEvent {
    }

}
