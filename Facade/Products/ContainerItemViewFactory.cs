using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class ContainerItemViewFactory :AbstractViewFactory<ContainerItemData, ContainerItem, ContainerItemView> {
        protected internal override ContainerItem toObject(ContainerItemData d) => new(d);
    }
}
