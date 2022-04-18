using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Products {
    public sealed class ProductSetView :EntityBaseView {
        [DisplayName("Package Type")]
        public string PackageTypeId { get; set; }
        public override string ToString() => new ProductSetViewFactory().Create(this).ToString();
    }
}
