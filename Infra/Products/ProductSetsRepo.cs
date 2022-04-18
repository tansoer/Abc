using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Infra.Common;

namespace Abc.Infra.Products {

    public sealed class ProductSetsRepo : EntityRepo<ProductSet, ProductSetData>,
        IProductSetsRepo {
        public ProductSetsRepo(ProductDb c = null) : base(c, c?.ProductSets) { }
    }
}