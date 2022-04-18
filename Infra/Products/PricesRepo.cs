using Abc.Data.Products;
using Abc.Domain.Products.Price;
using Abc.Infra.Common;

namespace Abc.Infra.Products {

    public sealed class PricesRepo : PagedRepo<IPrice, PriceData>,
        IPricesRepo {
        public PricesRepo(ProductDb c = null) : base(c, c?.Prices) { }
        protected internal override BasePrice toDomainObject(PriceData d) => PriceFactory.Create(d);
    }
}