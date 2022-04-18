using Abc.Data.Common;

namespace Abc.Data.Orders {
    public sealed class DiscountData :EntityBaseData {
        public string DiscountTypeId { get; set; }
        public DiscountsType DiscountType { get; set; }
        public string OrderEventId { get; set; }
        public string CurrencyId { get; set; }
        public decimal Amount { get; set; }
    }
}
