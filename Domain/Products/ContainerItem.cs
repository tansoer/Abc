using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products {
    public sealed class ContainerItem: Entity<ContainerItemData> {
        public ContainerItem() : this(null) { }
        public ContainerItem(ContainerItemData d) : base(d) { }
        public string ContainerId => get(Data?.ContainerId);
        public string ProductId => get(Data?.ProductId);
        public IProduct Product => item<IProductsRepo, IProduct>(ProductId);
        public byte RowNumber => get(Data?.RowNumber);
        public byte ColumnNumber => get(Data?.ColumnNumber);
        public byte LevelNumber => get(Data?.LevelNumber);
    }

    public interface IContainerItemsRepo:IRepo<ContainerItem> { };
}
