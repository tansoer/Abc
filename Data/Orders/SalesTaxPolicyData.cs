using Abc.Data.Common;

namespace Abc.Data.Orders {
    public sealed class SalesTaxPolicyData: EntityBaseData {
        public string OrderManagerId { get; set; }
        public decimal TaxationRate { get; set; }
        public string TaxationTypeId { get; set; }
    }
}
