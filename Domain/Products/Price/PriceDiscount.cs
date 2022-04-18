using Abc.Data.Products;
using Abc.Domain.Orders.Discounts;

namespace Abc.Domain.Products.Price {
    public sealed class PriceDiscount :BasePrice {
        public PriceDiscount() : this(null) { }
        public PriceDiscount(PriceData d) : base(d) { }
        internal string discountId => get(Data?.DiscountId);
        internal string priceId => get(Data?.PriceId);
        public IDiscount Discount => item<IDiscountsRepo, IDiscount>(discountId);
        public IPrice Price => item<IPricesRepo, IPrice>(priceId);
    }
}