using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products.Price;
using System;

namespace Abc.Domain.Orders.Discounts {

    public interface IDiscount :IEntity<DiscountData> {
        public string DiscountTypeId { get; }
        public string OrderEventId { get; }
        public IDiscountType DiscountType { get; }
        public IPrice Calculate(IPrice p);
    }
    public interface IDiscountsRepo :IRepo<IDiscount> {
    }

    public abstract class Discount :Entity<DiscountData>, IDiscount {
        protected Discount() : this(null) { }
        protected Discount(DiscountData d) : base(d) { }
        public string DiscountTypeId => get(Data?.DiscountTypeId);
        public string OrderEventId => get(Data?.OrderEventId);
        public IDiscountType DiscountType
            => item<IDiscountTypesRepo, IDiscountType>(DiscountTypeId);
        public IPrice Calculate(IPrice p) {
            var amount = get(p?.Amount?.ToString());
            var d = calculatePrice(p);
            d.PriceId = p.Id;
            d.DiscountId = Id;
            d.ValidFrom = DateTime.Now;
            d.Name = $"{Name} for {amount}";
            d.Code = $"{Code} for {amount}";
            d.Details = $"{Details} for {amount}";
            return new PriceDiscount(d); 
        }
        protected internal virtual PriceData calculatePrice(IPrice p) 
            => new() {
                Amount = 0M,
                CurrencyId = get(p?.Amount?.Currency?.Id)
            };
    }
}

