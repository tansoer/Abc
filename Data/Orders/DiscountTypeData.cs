using Abc.Data.Common;

namespace Abc.Data.Orders {
    public sealed class DiscountTypeData :EntityTypeData {
        public string OrderManagerId { get; set; }
        public string EligibilityRuleSetId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyId { get; set; }
    }
}
