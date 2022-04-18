using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products.Price {

    public abstract class AppliedPrice : BasePrice {

        protected AppliedPrice(PriceData d = null) : base(d) { }

        public string ProductId => Data?.ProductId ?? Unspecified.String;
        public IProduct Product =>
            new GetFrom<IProductsRepo, IProduct>().ById(ProductId);

    }

}