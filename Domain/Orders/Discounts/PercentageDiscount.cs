using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Domain.Products.Price;
using Abc.Domain.Quantities;

namespace Abc.Domain.Orders.Discounts {
    public sealed class PercentageDiscount :Discount {
        public PercentageDiscount() : this(null) { }
        public PercentageDiscount(DiscountData d) : base(d) { }
        internal decimal amount => get(Data?.Amount);
        internal string unitId => get(Data?.CurrencyId);
        internal Unit unit => item<IUnitsRepo, Unit>(unitId);
        internal Quantity Amount => new(decimal.ToDouble(amount), unit);
        protected internal override PriceData calculatePrice(IPrice p) {
            var d = base.calculatePrice(p);
            var a = p.Amount.Multiply(amount);
            d.Amount = a.Amount;
            return d;
        }
    }
}

