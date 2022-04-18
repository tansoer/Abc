using Abc.Data.Orders;
using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Orders {
    public sealed class DiscountView : EntityBaseView {
        [DisplayName("Discount type")] public string DiscountTypeId { get; set; }
        [DisplayName("Discounts type")] public DiscountsType DiscountType { get; set; }
        [DisplayName("Order event")] public string OrderEventId { get; set; }
        [DisplayName("Currency")] public string CurrencyId { get; set; }
        public decimal Amount { get; set; }
    }
}
