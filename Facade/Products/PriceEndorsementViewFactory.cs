using Abc.Data.Products;
using Abc.Domain.Products.Price;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class PriceEndorsementViewFactory :AbstractViewFactory<PriceEndorsementData, PriceEndorsement, PriceEndorsementView> {
        protected internal override PriceEndorsement toObject(PriceEndorsementData d) => new(d);
    }
}
