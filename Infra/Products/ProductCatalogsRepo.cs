using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Infra.Common;

namespace Abc.Infra.Products {

    public sealed class ProductCatalogsRepo : EntityRepo<ProductCatalog, ProductCatalogData>,
        IProductCatalogsRepo {
        public ProductCatalogsRepo(ProductDb c = null) : base(c, c?.ProductCatalogs) { }
    }
}