using Abc.Data.Products;

namespace Abc.Domain.Products.Price {

    public sealed class PriceError : AppliedPrice {

        public PriceError(PriceData d = null) : base(d) { }

    }

}