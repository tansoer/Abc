using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class CatalogedProductViewFactory :AbstractViewFactory<CatalogedProductData, CatalogedProduct, CatalogedProductView> {
        protected internal override CatalogedProduct toObject(CatalogedProductData d) => new(d);
    }
}
