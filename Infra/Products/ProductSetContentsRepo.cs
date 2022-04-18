using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Infra.Common;

namespace Abc.Infra.Products {
    public sealed class ProductSetContentsRepo : EntityRepo<ProductSetContent, ProductSetContentData>,
        IProductSetContentsRepo {
        public ProductSetContentsRepo(ProductDb c = null) : base(c, c?.ProductSetContents) { }
    }
}