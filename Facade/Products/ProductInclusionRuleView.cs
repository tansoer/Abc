using Abc.Data.Products;
using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Products {
    public sealed class ProductInclusionRuleView :EntityBaseView {
        [DisplayName("Inclusion Kind")]
        public ProductInclusionKind InclusionKind { get; set; }
        [DisplayName("Minimum amount")]
        public double MinimumAmount { get; set; }
        [DisplayName("Maximum amount")]
        public double MaximumAmount { get; set; }
        [DisplayName("Unit")]
        public string UnitId { get; set; }
        [DisplayName("Product Set")]
        public string ProductSetId { get; set; }
        [DisplayName("Package Type")]
        public string PackageTypeId { get; set; }
        [DisplayName("Master Inclusion Rule")]
        public string ProductInclusionId { get; set; }
        public override string ToString() => new ProductInclusionRuleViewFactory().Create(this).ToString();
    }
}
