using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products.Catalog {
    public sealed class ProductCategory : Entity<ProductCategoryData> {
        public ProductCategory() : this(null) { }
        public ProductCategory(ProductCategoryData d) : base(d) { }
        public string BaseCategoryId => data?.BaseCategoryId ?? Unspecified.String;
        public ProductCategory BaseCategory =>
            new GetFrom<IProductCategoriesRepo, ProductCategory>().ById(BaseCategoryId);
    }
}