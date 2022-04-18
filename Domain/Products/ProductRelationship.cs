using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products.Packets;

namespace Abc.Domain.Products {
    public sealed class ProductRelationship : Relationship<ProductRelationshipData> {
        public ProductRelationship() : this(null) { }
        public ProductRelationship(ProductRelationshipData d) : base(d) { }
        public ProductRelationshipType Type =>
            item<IProductRelationshipTypesRepo, ProductRelationshipType>(RelationshipTypeId);
        public IProduct Consumer => item<IProductsRepo, IProduct>(ConsumerEntityId);
        public IProduct Provider => item<IProductsRepo, IProduct>(ProviderEntityId);
    }
}