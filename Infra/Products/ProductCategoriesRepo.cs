using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Infra.Common;

namespace Abc.Infra.Products {
    public sealed class ProductCategoriesRepo : EntityRepo<ProductCategory, ProductCategoryData>,
        IProductCategoriesRepo {
        public ProductCategoriesRepo(ProductDb c = null) : base(c, c?.ProductCategories) { }
    }
}