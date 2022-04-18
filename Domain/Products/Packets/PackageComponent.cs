using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products.Packets {

    public sealed class PackageComponent : Entity<PackageComponentData> {
        public PackageComponent() : this(null) { }
        public PackageComponent(PackageComponentData d) : base(d) { }
        public string ProductTypeId => data?.ProductTypeId ?? Unspecified.String;
        public string PackageTypeId => data?.PackageTypeId ?? Unspecified.String;
        public IProductType ProductType =>
            new GetFrom<IProductTypesRepo, IProductType>().ById(ProductTypeId);
        public PackageType PackageType =>
            new GetFrom<IProductTypesRepo, IProductType>().ById(PackageTypeId) as PackageType;
    }
}