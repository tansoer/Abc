using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {
    public sealed class DeliveryTypesRepo :PagedRepo<DeliveryType, DeliveryTypeData>,
        IDeliveryTypesRepo {

        public DeliveryTypesRepo(OrderDb c = null) : base(c, c?.DeliveryTypes) { }

        protected internal override DeliveryType toDomainObject(DeliveryTypeData d) 
            => new DeliveryType(d);
    }
}