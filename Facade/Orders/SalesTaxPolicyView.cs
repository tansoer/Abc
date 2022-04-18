using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Orders
{
    public sealed class SalesTaxPolicyView : EntityBaseView
    {
        [DisplayName("Order manager")] public string OrderManagerId { get; set; }
        [DisplayName("Taxation rate")] public decimal TaxationRate { get; set; }
        [DisplayName("Taxation type")] public string TaxationTypeId { get; set; }
    }
}
