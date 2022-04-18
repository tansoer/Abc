using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Infra.Common;

namespace Abc.Infra.Products {
    public sealed class ContainerItemsRepo: EntityRepo<ContainerItem, ContainerItemData>, IContainerItemsRepo {
        public ContainerItemsRepo(ProductDb c = null) : base(c, c.ContainerItems) { }
    }
}
