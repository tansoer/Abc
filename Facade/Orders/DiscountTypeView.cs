using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Orders
{
    public sealed class DiscountTypeView : EntityTypeView
    {
        [DisplayName("Order manager")] public string OrderManagerId { get; set; }
        [DisplayName("Eligibility rule")] public string EligibilityRuleSetId { get; set; }
        public decimal Amount { get; set; }
        [DisplayName("Currency")] public string CurrencyId { get; set; }
    }
}
