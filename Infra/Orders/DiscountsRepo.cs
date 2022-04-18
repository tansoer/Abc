using Abc.Data.Orders;
using Abc.Domain.Orders.Discounts;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {

    public sealed class DiscountsRepo :PagedRepo<IDiscount, DiscountData>,
        IDiscountsRepo {

        public DiscountsRepo(OrderDb c = null) : base(c, c?.Discounts) { }

        protected internal override IDiscount toDomainObject(DiscountData d) => DiscountFactory.Create(d);
    }
}