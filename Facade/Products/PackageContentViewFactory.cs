using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class PackageContentViewFactory
        :AbstractViewFactory<PackageContentData, PackageContent, PackageContentView> {
        protected internal override PackageContent toObject(PackageContentData d) => new(d);
    }
}
