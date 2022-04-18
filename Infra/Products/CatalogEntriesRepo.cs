using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Infra.Common;

namespace Abc.Infra.Products {

    public sealed class CatalogEntriesRepo : EntityRepo<CatalogEntry, CatalogEntryData>,
        ICatalogEntriesRepo {
        public CatalogEntriesRepo(ProductDb c = null) : base(c, c?.CatalogEntries) { }
    }
}