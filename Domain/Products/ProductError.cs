using Abc.Data.Products;

namespace Abc.Domain.Products {

    public sealed class ProductError : BaseProduct<IProductType> {

        public ProductError(ProductData d = null) : base(d) { }

        public override IProductType Type => type;

    }

}