using Abc.Data.Products;
using Abc.Domain.Products.Price;
using Abc.Infra.Common;

namespace Abc.Infra.Products {
    public sealed class PriceEndorsementsRepo : EntityRepo<PriceEndorsement, PriceEndorsementData>,
        IPriceEndorsementsRepo {
        public PriceEndorsementsRepo(ProductDb c = null) : base(c, c?.PriceEndorsements) { }
    }
}