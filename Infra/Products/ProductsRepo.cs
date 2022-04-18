using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Infra.Common;

namespace Abc.Infra.Products {

    public sealed class ProductsRepo : PagedRepo<IProduct, ProductData>,
        IProductsRepo {
        public ProductsRepo(ProductDb c = null) : base(c, c?.Products) { }
        protected internal override IProduct toDomainObject(ProductData d) => ProductFactory.Create(d);
    }
}