using System;
using Abc.Aids.Calculator;
using Abc.Data.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Quantities;

namespace Abc.Domain.Values {

    public static class ValueFactory {

        public static IValue Create(string name, object value) {
            switch (value) {
                case byte i: return new IntegerValue(name, i);
                case sbyte i: return new DecimalValue(name, i);
                case short i: return new IntegerValue(name, i);
                case ushort i: return new DecimalValue(name, i);
                case int i: return new IntegerValue(name, i);
                case uint i: return new DecimalValue(name, i);
                case long i: return new DecimalValue(name, i);
                case ulong i: return new DecimalValue(name, i);
                case double d: return new DoubleValue(name, d);
                case float d: return new DoubleValue(name, d);
                case decimal d: return new DecimalValue(name, d);
                case DateTime d: return new DateTimeValue(name, d);
                case bool b: return new BooleanValue(name, b);
                case Money m: return new MoneyValue(name, m);
                case Quantity q: return new QuantityValue(name, q);
                default: {
                        var s = value as string;
                        return new StringValue(name, s ?? value?.ToString());
                    }
            }
        }

        public static IValue Create(ValueData d) {
            if (d == null) return new ErrorValue();

            return d.ValueType switch
            {
                Data.Common.ValueType.Integer => new IntegerValue(d),
                Data.Common.ValueType.Double => new DoubleValue(d),
                Data.Common.ValueType.Decimal => new DecimalValue(d),
                Data.Common.ValueType.DateTime => new DateTimeValue(d),
                Data.Common.ValueType.Boolean => new BooleanValue(d),
                Data.Common.ValueType.String => new StringValue(d),
                Data.Common.ValueType.Money => new MoneyValue(d),
                Data.Common.ValueType.Quantity => new QuantityValue(d),
                _ => new ErrorValue(d)
            };
        }

    }

}


