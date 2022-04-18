using Abc.Data.Orders;

namespace Abc.Domain.Orders.Discounts {
    public sealed class UnspecifiedDiscount :Discount {
        public UnspecifiedDiscount() : this(null) { }
        public UnspecifiedDiscount(DiscountData d) : base(d) { }
    }
}