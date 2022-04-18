using Abc.Aids.Calculator;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using Abc.Data.Common;
using Abc.Domain.Values;
using ValueType = Abc.Data.Common.ValueType;

namespace Abc.Facade.Products {
    public static class FeatureSpecViewValueFactory {
        public static IValue Create(FeatureSpecView v) {
            return v.ValueType switch {
                ValueType.Boolean => booleanValueView(v),
                ValueType.String => stringValueView(v),
                ValueType.DateTime => dateTimeValueView(v),
                ValueType.Decimal => decimalValueView(v),
                ValueType.Double => doubleValueView(v),
                ValueType.Integer => integerValueView(v),
                ValueType.Money => moneyValueView(v),
                ValueType.Quantity => quantityValueView(v),
                _ => errorValueView(v)
            };
        }
        public static FeatureSpecView Create(FeatureSpecView v, IValue o) {
            return o switch {
                BooleanValue x => booleanValue(v, x),
                StringValue x => stringValue(v, x),
                IntegerValue x => integerValue(v, x),
                DoubleValue x => doubleValue(v, x),
                DecimalValue x => decimalValue(v, x),
                DateTimeValue x => dateTimeValue(v, x),
                MoneyValue x => moneyValue(v, x),
                QuantityValue x => quantityValue(v, x),
                _ => errorValue(v, o as ErrorValue)
            };
        }
        internal static QuantityValue quantityValueView(FeatureSpecView v)
            => value(ValueType.Quantity, v.DoubleValue, v.UnitId, v) as QuantityValue;
        private static IValue value(ValueType t, object o, string id, FeatureSpecView v) {
            if (v.ValueType != t) return null;
            var d = new ValueData();
            Copy.Members(v, d);
            d.Value = Variable.ToString(o);
            d.UnitOrCurrencyId = id;
            return ValueFactory.Create(d);
        }
        private static MoneyValue moneyValueView(FeatureSpecView v)
            => value(ValueType.Money, v.DecimalValue, v.CurrencyId, v) as MoneyValue;
        private static IntegerValue integerValueView(FeatureSpecView v)
            => value(ValueType.Integer, v.IntegerValue, null, v) as IntegerValue;
        private static DoubleValue doubleValueView(FeatureSpecView v)
            => value(ValueType.Double, v.DoubleValue, null, v) as DoubleValue;
        private static DecimalValue decimalValueView(FeatureSpecView v)
            => value(ValueType.Decimal, v.DecimalValue, null, v) as DecimalValue;
        private static DateTimeValue dateTimeValueView(FeatureSpecView v)
            => value(ValueType.DateTime, v.DateTimeValue, null, v) as DateTimeValue;
        private static ErrorValue errorValueView(FeatureSpecView v) {
            var x = value(ValueType.Error, v.StringValue, null, v)
                    ?? value(ValueType.Unspecified, v.StringValue, null, v);
            return x as ErrorValue;
        }
        private static StringValue stringValueView(FeatureSpecView v)
            => value(ValueType.String, v.StringValue, null, v) as StringValue;
        private static BooleanValue booleanValueView(FeatureSpecView v)
            => value(ValueType.Boolean, v.BooleanValue, null, v) as BooleanValue;
        private static FeatureSpecView quantityValue(FeatureSpecView v, QuantityValue o) {
            v.ValueType = ValueType.Quantity;
            v.DoubleValue = o.Value.Amount;
            v.UnitId = o.UnitId;
            return v;
        }
        private static FeatureSpecView moneyValue(FeatureSpecView v, MoneyValue o) {
            v.ValueType = ValueType.Money;
            v.DecimalValue = o.Value.Amount;
            v.CurrencyId = o.CurrencyId;
            return v;
        }
        private static FeatureSpecView dateTimeValue(FeatureSpecView v, DateTimeValue o) {
            v.ValueType = ValueType.DateTime;
            v.DateTimeValue = o.Value;
            return v;
        }
        private static FeatureSpecView decimalValue(FeatureSpecView v, DecimalValue o) {
            v.ValueType = ValueType.Decimal;
            v.DecimalValue = o.Value;
            return v;
        }
        private static FeatureSpecView doubleValue(FeatureSpecView v, DoubleValue o) {
            v.ValueType = ValueType.Double;
            v.DoubleValue = o.Value;
            return v;
        }
        private static FeatureSpecView integerValue(FeatureSpecView v, IntegerValue o) {
            v.ValueType = ValueType.Integer;
            v.IntegerValue = o.Value;
            return v;
        }
        private static FeatureSpecView errorValue(FeatureSpecView v, ErrorValue o) {
            v.ValueType = ValueType.Error;
            v.StringValue = o.Value;
            return v;
        }
        private static FeatureSpecView stringValue(FeatureSpecView v, StringValue o) {
            v.ValueType = ValueType.String;
            v.StringValue = o.Value;
            return v;
        }
        private static FeatureSpecView booleanValue(FeatureSpecView v, BooleanValue o) {
            v.ValueType = ValueType.Boolean;
            v.BooleanValue = o.Value;
            return v;
        }
    }
}