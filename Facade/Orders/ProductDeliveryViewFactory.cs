using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Facade.Common;

namespace Abc.Facade.Orders {
    public sealed class ProductDeliveryViewFactory
        :AbstractViewFactory<ProductDeliveryData, ProductDelivery, ProductDeliveryView> {
        protected internal override ProductDelivery toObject(ProductDeliveryData d) =>
            new(d);
    }
}