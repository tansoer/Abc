using System;
using Abc.Aids.Enums;
using Abc.Data.Rules;
using Abc.Domain.Currencies;
using Abc.Domain.Quantities;

namespace Abc.Domain.Rules {

    public static class VariableFactory {

        public static IVariable Create(string name, object value, string ruleId = null, bool isContextId = false) {
            switch (value) {
                case byte i: return new IntegerVariable(name, i, ruleId, isContextId);
                case sbyte i: return new DecimalVariable(name, i, ruleId, isContextId);
                case short i: return new IntegerVariable(name, i, ruleId, isContextId);
                case ushort i: return new DecimalVariable(name, i, ruleId, isContextId);
                case int i: return new IntegerVariable(name, i, ruleId, isContextId);
                case uint i: return new DecimalVariable(name, i, ruleId, isContextId);
                case long i: return new DecimalVariable(name, i, ruleId, isContextId);
                case ulong i: return new DecimalVariable(name, i, ruleId, isContextId);
                case double d: return new DoubleVariable(name, d, ruleId, isContextId);
                case float d: return new DoubleVariable(name, d, ruleId, isContextId);
                case decimal de: return new DecimalVariable(name, de, ruleId, isContextId);
                case DateTime dt: return new DateTimeVariable(name, dt, ruleId, isContextId);
                case bool b: return new BooleanVariable(name, b, ruleId, isContextId);
                case Money m: return new MoneyVariable(name, m, ruleId, isContextId);
                case Quantity q: return new QuantityVariable(name, q, ruleId, isContextId);
                default: {
                        var s = value as string;
                        return new StringVariable(name, s ?? value?.ToString(), ruleId, isContextId);
                    }
            }
        }

        public static IVariable Create(RuleElementData d) {
            if (d == null) return new UnspecifiedVariable();

            return d.RuleElementType switch
            {
                RuleElementType.Integer => new IntegerVariable(d),
                RuleElementType.Double => new DoubleVariable(d),
                RuleElementType.Decimal => new DecimalVariable(d),
                RuleElementType.DateTime => new DateTimeVariable(d),
                RuleElementType.Boolean => new BooleanVariable(d),
                RuleElementType.String => new StringVariable(d),
                RuleElementType.Money => new MoneyVariable(d),
                RuleElementType.Quantity => new QuantityVariable(d),
                RuleElementType.Error => new RuleError(d),
                _ => new UnspecifiedVariable(d)
            };
        }

    }

}


