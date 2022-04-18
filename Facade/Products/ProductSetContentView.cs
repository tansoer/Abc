using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Products {
    public sealed class ProductSetContentView :EntityBaseView {
        [DisplayName("Product Type")]
        public string ProductTypeId { get; set; }
        [DisplayName("Product Set")]
        public string ProductSetId { get; set; }
    }
}
