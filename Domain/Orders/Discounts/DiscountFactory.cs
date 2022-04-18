using Abc.Data.Orders;

namespace Abc.Domain.Orders.Discounts {
    public class DiscountFactory {
        public static IDiscount Create(DiscountData d) => d?.DiscountType switch {
                DiscountsType.Monetary => new MonetaryDiscount(d),
                DiscountsType.Percentage => new PercentageDiscount(d),
                _ => new UnspecifiedDiscount(d)
            };
    }
}