using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class CatalogEntryView : EntityBaseView {
        [DisplayName("Catalog")]
        public string CatalogId { get; set; }

        [DisplayName("Category")]
        public string CategoryId { get; set; }
    }
}
