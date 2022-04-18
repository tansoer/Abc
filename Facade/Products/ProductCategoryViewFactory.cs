using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class ProductCategoryViewFactory :AbstractViewFactory<ProductCategoryData, ProductCategory, ProductCategoryView> {
        protected internal override ProductCategory toObject(ProductCategoryData d) => new(d);
    }

}