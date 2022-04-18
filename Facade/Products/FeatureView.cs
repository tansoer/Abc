using System.ComponentModel;

namespace Abc.Facade.Products {

    public sealed class FeatureView : FeatureValueView {
        [DisplayName("Product")]
        public string ProductId { get; set; }
    }
}