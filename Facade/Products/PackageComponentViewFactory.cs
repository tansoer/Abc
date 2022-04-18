using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class PackageComponentViewFactory :AbstractViewFactory<
        PackageComponentData, PackageComponent, PackageComponentView> {
        protected internal override PackageComponent toObject(PackageComponentData d) => new (d);
    }
}
