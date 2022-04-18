using Abc.Data.Products;

namespace Abc.Domain.Products {

    public sealed class Product : BaseProduct<ProductType> {


        public Product(ProductData d = null) : base(d) { }

        public override ProductType Type => type as ProductType;
    }
}
