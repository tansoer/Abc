using Abc.Data.Common;

namespace Abc.Data.Products {

    public sealed class FeatureTypeData : EntityTypeData {
        public string ProductTypeId { get; set; }
        public bool IsMandatory { get; set; }
        public int NumericCode { get; set; }
        public bool MustSatisfyAll  { get; set; }
        public ValueType ValueType{ get; set; }
    }
}
