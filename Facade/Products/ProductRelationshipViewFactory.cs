using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class ProductRelationshipViewFactory :AbstractViewFactory<
    ProductRelationshipData, ProductRelationship, ProductRelationshipView> {
        protected internal override ProductRelationship toObject(ProductRelationshipData d) => new (d);
    }
}
