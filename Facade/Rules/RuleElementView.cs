using System;
using System.ComponentModel;
using Abc.Aids.Calculator;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;

namespace Abc.Facade.Rules {

    public sealed class RuleElementView : CommonRuleIdView {

        public int Index { get; set; }

        [DisplayName("Rule Context")]
        public string RuleContextId { get; set; }

        [DisplayName("Unit")]
        public string UnitId { get; set; }

        [DisplayName("Currency")]
        public string CurrencyId { get; set; }

        [DisplayName("Element Type")]
        public RuleElementType RuleElementType { get; set; }

        [DisplayName("Value")]
        public string StringValue { get; set; }

        [DisplayName("Value")]
        public int IntegerValue { get; set; }

        [DisplayName("Value")]
        public bool BooleanValue { get; set; }

        [DisplayName("Value")]
        public DateTime DateTimeValue { get; set; }

        [DisplayName("Value")]
        public double DoubleValue { get; set; }

        [DisplayName("Value")]
        public decimal DecimalValue { get; set; }

        public Operation Operation { get; set; }

        public string Value => RuleElementType switch
        {
            RuleElementType.Operator => Variable.ToString(Operation),
            RuleElementType.Boolean => Variable.ToString(BooleanValue),
            RuleElementType.String => StringValue,
            RuleElementType.Integer => Variable.ToString(IntegerValue),
            RuleElementType.Decimal => Variable.ToString(DecimalValue),
            RuleElementType.Double => Variable.ToString(DoubleValue),
            RuleElementType.DateTime => Variable.ToString(DateTimeValue),
            RuleElementType.Quantity => $"{Variable.ToString(DoubleValue)} {UnitId}",
            RuleElementType.Money => $"{Variable.ToString(DecimalValue)} {CurrencyId}",
            RuleElementType.Error => StringValue,
            _ => String.Empty
        };
    }
}

