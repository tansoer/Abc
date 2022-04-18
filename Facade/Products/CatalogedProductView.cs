using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class CatalogedProductView : EntityBaseView {
        [DisplayName("Catalog Entry")] public string CatalogEntryId { get; set; }
        [DisplayName("Product Type")] public string ProductTypeId { get; set; }
    }
}
