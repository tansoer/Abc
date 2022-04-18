using Abc.Data.Orders;
using Abc.Domain.Common;

namespace Abc.Domain.Orders.Delivery {
    public sealed class ProductDelivery: Entity<ProductDeliveryData> {
        public ProductDelivery() : this(null) { }
        public ProductDelivery(ProductDeliveryData d) : base(d) { }
        public string DeliveryTypeId => get(Data?.DeliveryTypeId);
        public DeliveryType DeliveryType => item<IDeliveryTypesRepo, DeliveryType>(DeliveryTypeId);
    }
    public interface IProductDeliveriesRepo :IRepo<ProductDelivery> {
    }


}
