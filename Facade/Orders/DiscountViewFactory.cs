using Abc.Data.Orders;
using Abc.Domain.Orders.Discounts;
using Abc.Facade.Common;

namespace Abc.Facade.Orders
{
    public sealed class DiscountViewFactory 
        :AbstractViewFactory<DiscountData, IDiscount, DiscountView> {
        protected internal override IDiscount toObject(DiscountData d) =>
            DiscountFactory.Create(d);

    }
}