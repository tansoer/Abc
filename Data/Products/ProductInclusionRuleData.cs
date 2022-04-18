using Abc.Data.Common;

namespace Abc.Data.Products {

    public sealed class ProductInclusionRuleData : EntityBaseData {

        public ProductInclusionKind InclusionKind { get; set; }
        public double MinimumAmount { get; set; }
        public double MaximumAmount { get; set; }
        public string UnitId { get; set; }
        public string ProductSetId { get; set; }
        public string PackageTypeId { get; set; }
        public string ProductInclusionId { get; set; }

    }

}
