using Abc.Data.Orders;
using Abc.Domain.Common;

namespace Abc.Domain.Orders.Delivery {
    public sealed class DeliveryType :EntityType<IDeliveryTypesRepo, DeliveryType, DeliveryTypeData> {
        public DeliveryType() : this(null) { }
        public DeliveryType(DeliveryTypeData d) : base(d) { }
    }
    public interface IDeliveryTypesRepo :IRepo<DeliveryType> {
    }

}
