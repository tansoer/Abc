using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products.Packets {

    public sealed class ProductSetContent : Entity<ProductSetContentData> {
        public ProductSetContent() : this(null) { }
        public ProductSetContent(ProductSetContentData d) : base(d) { }
        public string ProductTypeId => Data?.ProductTypeId ?? Unspecified.String;
        public string ProductSetId => Data?.ProductSetId ?? Unspecified.String;
        public IProductType ProductType =>
            new GetFrom<IProductTypesRepo, IProductType>().ById(ProductTypeId);
        public ProductSet ProductSet =>
            new GetFrom<IProductSetsRepo, ProductSet>().ById(ProductSetId);
    }
}