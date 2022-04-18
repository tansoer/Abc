using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Infra.Common;

namespace Abc.Infra.Products {
    public sealed class PackageContentsRepo :EntityRepo<PackageContent, PackageContentData>,
        IPackageContentsRepo {
        public PackageContentsRepo(ProductDb c = null) : base(c, c?.PackageContents) { }
    }
}