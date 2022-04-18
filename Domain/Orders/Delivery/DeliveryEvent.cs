using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using System.Collections.Generic;

namespace Abc.Domain.Orders.Delivery {
    public abstract class DeliveryEvent :OrderEvent, IDeliveryEvent {
        protected DeliveryEvent() : this(null) { }
        protected DeliveryEvent(OrderEventData d) : base(d) { }
        public virtual IReadOnlyList<IDeliveryOrderLine> DeliveryLines => list<IDeliveryOrderLine, IOrderLine>(OrderLines);
        public void RejectProductInstances(string reason, string orderLineId, params string[] productIDs) {
            foreach (var l in DeliveryLines) {
                l.RejectProductInstances(reason, orderLineId, productIDs);
            }
        }
        public string DeliveryId => get(Data?.ProductDeliveryId);
        public ProductDelivery Delivery => item<IProductDeliveriesRepo, ProductDelivery>(DeliveryId);
    }
    public interface IDeliveryEvent :IOrderEvent {

    }
}
