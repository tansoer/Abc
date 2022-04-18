using Abc.Data.Products;

namespace Abc.Domain.Products {
    public sealed class ContainerType : BaseProductType {
        public ContainerType(ProductTypeData d = null) : base(d) { }
        public byte Columns => Data?.ColumnsCount ?? 0;
        public byte Rows => Data?.RowsCount ?? 0;
        public byte Levels => Data?.LevelsCount ?? 0;
    }
}