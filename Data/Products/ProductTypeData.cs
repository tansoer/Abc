using System;
using Abc.Data.Common;

namespace Abc.Data.Products {

    public sealed class ProductTypeData : EntityTypeData {
        public ProductKind ProductKind { get; set; }
        public PricingStrategy PricingStrategy { get; set; }
        public double Amount { get; set; }
        public string UnitId { get; set; }
        public DateTime? PeriodOfOperationFrom { get; set; }
        public DateTime? PeriodOfOperationTo { get; set; }
        public byte ColumnsCount { get; set; }
        public byte RowsCount { get; set; }
        public byte LevelsCount { get; set; }
        public string AlternativeCodes { get; set; }
        public bool IsBaseType { get; set; }
    }
}
