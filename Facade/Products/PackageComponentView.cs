using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Products {
    public sealed class PackageComponentView :EntityBaseView {
        [DisplayName("Product Type")]
        public string ProductTypeId { get; set; }
        [DisplayName("Package Type")]
        public string PackageTypeId { get; set; }
    }
}
