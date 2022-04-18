using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class ProductSetViewFactory :AbstractViewFactory<
        ProductSetData, ProductSet, ProductSetView> {
        protected internal override ProductSet toObject(ProductSetData d) => new(d);
    }
}
