using Abc.Data.Products;

namespace Abc.Domain.Products {

    public sealed class UniqueProduct : PoleomorphProduct<UniqueProductType> {
        public UniqueProduct(ProductData d = null) : base(d) { }
        public override UniqueProductType Type => type as UniqueProductType;
        public override string Id => Type?.Id ?? get(Data?.Id);
    }
}