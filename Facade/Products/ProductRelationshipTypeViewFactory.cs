using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class ProductRelationshipTypeViewFactory :AbstractViewFactory<
        ProductRelationshipTypeData, ProductRelationshipType, ProductRelationshipTypeView> {
        protected internal override ProductRelationshipType toObject(ProductRelationshipTypeData d) => new (d);
    }
}
