using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Products {
    public sealed class ContainerItemView :EntityBaseView {
        [DisplayName("Container")]
        public string ContainerId { get; set; }
        [DisplayName("Product")]
        public string ProductId { get; set; }
        [DisplayName("Row Number")]
        public byte RowNumber { get; set; }
        [DisplayName("Column Number")]
        public byte ColumnNumber { get; set; }
        [DisplayName("Level Number")]
        public byte LevelNumber { get; set; }
    }
}
