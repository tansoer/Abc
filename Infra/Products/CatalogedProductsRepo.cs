using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Infra.Common;

namespace Abc.Infra.Products {

    public sealed class CatalogedProductsRepo :EntityRepo<CatalogedProduct, CatalogedProductData>,
        ICatalogedProductsRepo {
        public CatalogedProductsRepo(ProductDb c = null) : base(c, c?.CatalogedProducts) { }
    }
}