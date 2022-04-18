using Abc.Data.Orders;
using Abc.Domain.Orders.Discounts;
using Abc.Facade.Common;

namespace Abc.Facade.Orders {
    public sealed class DiscountTypeViewFactory
        :AbstractViewFactory<DiscountTypeData, DiscountType, DiscountTypeView> {
        protected internal override DiscountType toObject(DiscountTypeData d) =>
            new (d);
    }
}