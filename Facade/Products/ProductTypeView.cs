using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abc.Data.Products;
using Abc.Facade.Attributes;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class ProductTypeView : EntityTypeView {
        [DisplayName("Product type")] public ProductKind ProductKind { get; set; }
        [DisplayName("Pricing strategy")] public PricingStrategy PricingStrategy { get; set; }
        [DisplayName("Amount")] public double Amount { get; set; }
        [DisplayName("Unit")] public string UnitId { get; set; }
        [DataType(DataType.Date)] [DisplayName("Period of operation from")] public DateTime? PeriodOfOperationFrom { get; set; }
        [DataType(DataType.Date)] [DisplayName("Period of operation to")] public DateTime? PeriodOfOperationTo { get; set; }
        [DisplayName("Alternative codes")] public string AlternativeCodes { get; set; }
        [DisplayName("Container columns count")] public byte ColumnsCount { get; set; }
        [DisplayName("Container rows count")] public byte RowsCount { get; set; }
        [DisplayName("Container levels count")] public byte LevelsCount { get; set; }
        [DisplayName("Is base type")] public bool IsBaseType { get; set; }
        [Required][Unique(typeof(Domain.Products.IProductTypesRepo))][DisplayName("Code")] public new string Code { get; set; }
    }
}