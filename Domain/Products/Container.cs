using Abc.Data.Products;
using System.Collections.Generic;

namespace Abc.Domain.Products {

    public sealed class Container : BaseProduct<ContainerType> {
        internal static string containerId
            => nameOf<ContainerItemData>(x => x.ContainerId);
        public Container(ProductData d = null) : base(d) { }
        public override ContainerType Type => type as ContainerType;
        public IReadOnlyList<ContainerItem> Items => 
            list<IContainerItemsRepo, ContainerItem>(containerId, Id);
    }
}