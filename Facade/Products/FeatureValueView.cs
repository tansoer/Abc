using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public abstract class FeatureValueView : EntityBaseView {
        [DisplayName("Feature type")] public string FeatureTypeId { get; set; }
        [DisplayName("Value")] public string ValueSpecificationId { get; set; }
    }
}