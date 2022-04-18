using Abc.Data.Common;

namespace Abc.Data.Products {
    public sealed class CatalogedProductData :EntityBaseData {
        public string CatalogEntryId { get; set; }
        public string ProductTypeId { get; set; }

    }
}
