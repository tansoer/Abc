using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Infra.Common;

namespace Abc.Infra.Products {
    public sealed class PackageComponentsRepo : EntityRepo<PackageComponent, PackageComponentData>,
        IPackageComponentsRepo {
        public PackageComponentsRepo(ProductDb c = null) : base(c, c?.PackageComponents) { }
    }
}