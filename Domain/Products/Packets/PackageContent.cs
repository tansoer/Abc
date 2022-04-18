using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products.Packets {

    public sealed class PackageContent : Entity<PackageContentData> {
        public PackageContent() : this(null) { }
        public PackageContent(PackageContentData d) : base(d) { }
        public string ProductId => data?.ProductId ?? Unspecified.String;
        public string PackageId => data?.PackageId ?? Unspecified.String;
        public IProduct Product =>
            new GetFrom<IProductsRepo, IProduct>().ById(ProductId);
        public Package Package =>
            new GetFrom<IProductsRepo, IProduct>().ById(PackageId) as Package;
    }
}