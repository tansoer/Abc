using Abc.Data.Common;

namespace Abc.Data.Products {

    public abstract class FeatureValueData : EntityBaseData {
        public string FeatureTypeId { get; set; }
        public string ValueSpecId { get; set; }
    }
}
