using System;
using System.ComponentModel;
using Abc.Aids.Extensions;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class FeatureSpecView :EntityBaseView {
        [DisplayName("Unit")] public string UnitId { get; set; }
        [DisplayName("Currency")] public string CurrencyId { get; set; }
        [DisplayName("Value type")] public Data.Common.ValueType ValueType { get; set; }
        [DisplayName("Value")] public string StringValue { get; set; }
        [DisplayName("Value")] public int IntegerValue { get; set; }
        [DisplayName("Value")] public bool BooleanValue { get; set; }
        [DisplayName("Value")] public DateTime DateTimeValue { get; set; }
        [DisplayName("Value")] public double DoubleValue { get; set; }
        [DisplayName("Value")] public decimal DecimalValue { get; set; }
        [DisplayName("Value")] public string FeatureValue => ToString();
        public override string ToString() => ValueType switch {
            Data.Common.ValueType.Boolean => Variable.ToString(BooleanValue),
            Data.Common.ValueType.String => StringValue,
            Data.Common.ValueType.Integer => Variable.ToString(IntegerValue),
            Data.Common.ValueType.Decimal => Variable.ToString(DecimalValue),
            Data.Common.ValueType.Double => Variable.ToString(DoubleValue),
            Data.Common.ValueType.DateTime => Variable.ToString(DateTimeValue),
            Data.Common.ValueType.Quantity => $"{Variable.ToString(DoubleValue)} {UnitId}",
            Data.Common.ValueType.Money => $"{Variable.ToString(DecimalValue)} {CurrencyId}",
            Data.Common.ValueType.Error => StringValue,
            Data.Common.ValueType.Unspecified => StringValue,
            _ => string.Empty
        };
    }
}