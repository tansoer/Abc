using Abc.Data.Common;

namespace Abc.Data.Products {

    public sealed class PriceData : EntityBaseData {

        public decimal Amount { get; set; }
        public string CurrencyId { get; set; }
        public string DiscountId { get; set; }
        public string PriceId { get; set; }
        public string ProductId { get; set; }
        public string PossiblePriceId { get; set; }
        public string RuleSetId { get; set; }
        public string RuleOverrideId { get; set; }
        public string ProductTypeId { get; set; }
        public PriceKind Kind { get; set; }

    }

}
