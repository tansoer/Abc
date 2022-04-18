using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Domain.Currencies;
using Abc.Domain.Products.Price;

namespace Abc.Domain.Orders.Discounts {
    public sealed class MonetaryDiscount :Discount {
        public MonetaryDiscount() : this(null) { }
        public MonetaryDiscount(DiscountData d) : base(d) { }
        internal string currencyId => get(Data?.CurrencyId);
        internal decimal amount => get(Data?.Amount);
        internal Currency currency => item<ICurrencyRepo, Currency>(currencyId);
        internal Money Amount => new(amount, currency);
        protected internal override PriceData calculatePrice(IPrice p) {
            var d = base.calculatePrice(p);
            var a = Amount.ConvertTo(p?.Amount?.Currency);
            d.Amount = a.Amount;
            return d;
        }
    }
}

