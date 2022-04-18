using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Currencies;

namespace Abc.Domain.Products.Price {

    public interface IPrice: IEntity<PriceData> {
        public Money Amount { get; }
    }
    public abstract class BasePrice : Entity<PriceData>, IPrice {
        protected BasePrice(PriceData d = null) : base(d) { }
        protected internal decimal amount => Data?.Amount ?? Unspecified.Decimal;
        protected internal string currencyId => Data?.CurrencyId ?? Unspecified.String;
        public Money Amount => new(amount, currencyId, ValidFrom);
    }
}