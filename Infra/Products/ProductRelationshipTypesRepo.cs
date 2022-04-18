using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Infra.Common;

namespace Abc.Infra.Products {

    public sealed class ProductRelationshipTypesRepo : EntityRepo<ProductRelationshipType, ProductRelationshipTypeData>,
        IProductRelationshipTypesRepo {
        public ProductRelationshipTypesRepo(ProductDb c = null) : base(c, c?.ProductRelationshipTypes) { }
    }
}