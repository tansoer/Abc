using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Facade.Common;

namespace Abc.Facade.Orders {
    public sealed class DeliveryTypeViewFactory
        :AbstractViewFactory<DeliveryTypeData, DeliveryType, DeliveryTypeView> {
        protected internal override DeliveryType toObject(DeliveryTypeData d) =>
            new(d);

    }
}