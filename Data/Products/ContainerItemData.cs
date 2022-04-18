using Abc.Data.Common;

namespace Abc.Data.Products {
    public sealed class ContainerItemData: EntityBaseData {
        public string ContainerId { get; set; }
        public string ProductId { get; set; }
        public byte RowNumber { get; set; }
        public byte ColumnNumber { get; set; }
        public byte LevelNumber { get; set; }
    }
}
