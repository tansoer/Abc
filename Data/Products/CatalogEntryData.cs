
using Abc.Data.Common;

namespace Abc.Data.Products {
    public sealed class CatalogEntryData : EntityBaseData {
        public string CatalogId { get; set; }
        public string CategoryId { get; set; }
    }
}
