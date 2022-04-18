using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Infra.Common;

namespace Abc.Infra.Products {
    public sealed class ProductTypesRepo : PagedRepo<IProductType, ProductTypeData>,
        IProductTypesRepo {
        public ProductTypesRepo(ProductDb c = null) : base(c, c?.ProductTypes) { }
        protected internal override IProductType toDomainObject(ProductTypeData d) => ProductTypeFactory.Create(d);
    }
}