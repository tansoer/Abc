using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class CatalogEntryViewFactory :AbstractViewFactory<CatalogEntryData, CatalogEntry, CatalogEntryView> {
        protected internal override CatalogEntry toObject(CatalogEntryData d) => new(d);
    }
}
