using System.Collections.Generic;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products.Catalog {
    public sealed class ProductCatalog : Entity<ProductCatalogData> {
        internal static string catalogId => GetMember.Name<CatalogEntryData>(x => x.CatalogId);
        public ProductCatalog() : this(null) { }
        public ProductCatalog(ProductCatalogData d) : base(d) { }
        public IReadOnlyList<CatalogEntry> CatalogEntries =>
            new GetFrom<ICatalogEntriesRepo, CatalogEntry>().ListBy(catalogId, Id);
    }
}
