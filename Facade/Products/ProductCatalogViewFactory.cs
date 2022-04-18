using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class ProductCatalogViewFactory :AbstractViewFactory<ProductCatalogData, ProductCatalog, ProductCatalogView> {
        protected internal override ProductCatalog toObject(ProductCatalogData d) => new(d);
    }
}
