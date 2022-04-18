using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {
    public sealed class ProductDeliveriesRepo :PagedRepo<ProductDelivery, ProductDeliveryData>,
        IProductDeliveriesRepo {

        public ProductDeliveriesRepo(OrderDb c = null) : base(c, c?.ProductDeliveries) { }

        protected internal override ProductDelivery toDomainObject(ProductDeliveryData d)
            => new (d);
    }
}