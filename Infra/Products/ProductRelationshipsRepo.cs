using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Infra.Common;

namespace Abc.Infra.Products {

    public sealed class ProductRelationshipsRepo : EntityRepo<ProductRelationship, ProductRelationshipData>,
        IProductRelationshipsRepo {
        public ProductRelationshipsRepo(ProductDb c = null) : base(c, c?.ProductRelationships) { }
    }
}