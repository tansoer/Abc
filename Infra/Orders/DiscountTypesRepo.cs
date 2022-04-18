using Abc.Data.Orders;
using Abc.Domain.Orders.Discounts;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {

    public sealed class DiscountTypesRepo :PagedRepo<IDiscountType, DiscountTypeData>,
        IDiscountTypesRepo {

        public DiscountTypesRepo(OrderDb c = null) : base(c, c?.DiscountTypes) { }

        protected internal override IDiscountType toDomainObject(DiscountTypeData d) 
            => new DiscountType(d);
    }
}