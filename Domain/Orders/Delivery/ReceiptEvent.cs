using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using System.Collections.Generic;

namespace Abc.Domain.Orders.Delivery {

    public sealed class ReceiptEvent :DeliveryEvent, IReceiptEvent {
        public ReceiptEvent() : this(null) { }
        public ReceiptEvent(OrderEventData d) : base(d) { }
        public IReadOnlyList<ReceiptLine> ReceiptLines => list<ReceiptLine, IDeliveryOrderLine>(DeliveryLines);
    }
    public interface IReceiptEvent :IDeliveryEvent {
    }
}
