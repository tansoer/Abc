using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Data.Processes;
using Abc.Facade.Common;
using System;
using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class AttributeValueView :EntityBaseView {
        [DisplayName("Process Element")] public string ProcessElementId { get; set; }
        [DisplayName("Attribute Type")] public string AttributeTypeId { get; set; }
        public Relation Equality { get; set; }
        public AttributeValueType Type { get; set; }
        [DisplayName("Unit")] public string UnitId { get; set; }
        [DisplayName("Currency")] public string CurrencyId { get; set; }
        [DisplayName("Value Type")] public Data.Common.ValueType ValueType { get; set; }
        [DisplayName("Value")] public string StringValue { get; set; }
        [DisplayName("Value")] public int IntegerValue { get; set; }
        [DisplayName("Value")] public bool BooleanValue { get; set; }
        [DisplayName("Value")] public DateTime DateTimeValue { get; set; }
        [DisplayName("Value")] public double DoubleValue { get; set; }
        [DisplayName("Value")] public decimal DecimalValue { get; set; }

        public string Value => ValueType switch {
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