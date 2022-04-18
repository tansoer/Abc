using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Products {
    public sealed class PackageContentView :EntityBaseView {
        [DisplayName("Product")]
        public string ProductId { get; set; }
        [DisplayName("Package")]
        public string PackageId { get; set; }
        [DisplayName("Component")]
        public string ComponentId { get; set; }
    }
}
