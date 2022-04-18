using Abc.Data.Products;
using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Products {
    public sealed class PriceView :EntityBaseView {
        [DisplayName("Amount")]
        public decimal Amount { get; set; }
        [DisplayName("Currency")]
        public string CurrencyId { get; set; }
        [DisplayName("Discount")]
        public string DiscountId { get; set; }
        [DisplayName("Price")]
        public string PriceId { get; set; }
        [DisplayName("Product")]
        public string ProductId { get; set; }
        [DisplayName("Possible Price")]
        public string PossiblePriceId { get; set; }
        [DisplayName("Rule Set")]
        public string RuleSetId { get; set; }
        [DisplayName("Rule Override")]
        public string RuleOverrideId { get; set; }
        [DisplayName("Product Type")]
        public string ProductTypeId { get; set; }
        [DisplayName("Price Kind")]
        public PriceKind Kind { get; set; }
    }
}
