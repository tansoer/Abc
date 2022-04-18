using Abc.Data.Products;
using Abc.Domain.Products.Price;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class PriceViewFactory :AbstractViewFactory<PriceData, IPrice, PriceView> {
        protected internal override IPrice toObject(PriceData d) => PriceFactory.Create(d);
    }
}
