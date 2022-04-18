using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class ProductViewFactory :AbstractViewFactory<ProductData, IProduct, ProductView> {
        protected internal override IProduct toObject(ProductData d) => ProductFactory.Create(d);
    }

}
