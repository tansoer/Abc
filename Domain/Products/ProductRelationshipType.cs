using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products {
    public sealed class ProductRelationshipType : RelationshipType<ProductRelationshipTypeData> {
        public ProductRelationshipType() : this(null) { }
        public ProductRelationshipType(ProductRelationshipTypeData d) : base(d) { }
        public IProductType Consumer =>
          new GetFrom<IProductTypesRepo, IProductType>().ById(ConsumerTypeId);
        public IProductType Provider =>
            new GetFrom<IProductTypesRepo, IProductType>().ById(ProviderTypeId);
    }

}
