using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class ProductSetContentViewFactory :AbstractViewFactory<
        ProductSetContentData, ProductSetContent, ProductSetContentView> {
        protected internal override ProductSetContent toObject(ProductSetContentData d)
            => new(d);
    }
}
