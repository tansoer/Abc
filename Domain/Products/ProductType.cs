using Abc.Data.Products;

namespace Abc.Domain.Products {

    public sealed class ProductType : BaseProductType {

        public ProductType() : this(null) { }
        public ProductType(ProductTypeData d = null) : base(d) { }

    }

}
