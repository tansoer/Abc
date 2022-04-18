using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using Abc.Data.Rules;
using Abc.Domain.Rules;

namespace Abc.Facade.Rules {

    public static class RuleElementViewFactory {

        public static IRuleElement Create(RuleElementView v) {
            var d = new RuleElementData();
            Copy.Members(v, d);

            return d.RuleElementType switch
            {
                RuleElementType.Operator => new Operator(d),
                RuleElementType.Operand => new Operand(d),
                _ => createVariable(d, v)
            };
        }

        public static RuleElementView Create(IRuleElement o) {
            var v = new RuleElementView();
            Copy.Members(o.Data, v);

            return o switch
            {
                BooleanVariable x => createView(v, x),
                DateTimeVariable x => createView(v, x),
                DecimalVariable x => createView(v, x),
                DoubleVariable x => createView(v, x),
                IntegerVariable x => createView(v, x),
                MoneyVariable x => createView(v, x),
                QuantityVariable x => createView(v, x),
                RuleError x => createView(v, x),
                StringVariable x => createView(v, x),
                Operator x => createView(v, x),
                _ => v,
            };
        }
        private static RuleElementView createView(RuleElementView v, Operator o) {
            v.Operation = o.Operation;
            return v;
        }
        private static RuleElementView createView(RuleElementView v, StringVariable o) {
            v.StringValue = o.Value;

            return v;
        }
        private static RuleElementView createView(RuleElementView v, RuleError o) {
            v.StringValue = o.Value;

            return v;
        }
        private static RuleElementView createView(RuleElementView v, QuantityVariable o) {
            v.DoubleValue = o.Value.Amount;
            v.UnitId = o.UnitId;
            return v;
        }
        private static RuleElementView createView(RuleElementView v, MoneyVariable o) {
            v.DecimalValue = o.Value.Amount;
            v.CurrencyId = o.CurrencyId;
            return v;
        }
        private static RuleElementView createView(RuleElementView v, IntegerVariable o) {
            v.IntegerValue = o.Value;

            return v;
        }
        private static RuleElementView createView(RuleElementView v, DoubleVariable o) {
            v.DoubleValue = o.Value;

            return v;
        }
        private static RuleElementView createView(RuleElementView v, DecimalVariable o) {
            v.DecimalValue = o.Value;

            return v;
        }

        private static RuleElementView createView(RuleElementView v, DateTimeVariable o) {
            v.DateTimeValue = o.Value;

            return v;
        }

        private static RuleElementView createView(RuleElementView v, BooleanVariable o) {
            v.BooleanValue = o.Value;

            return v;
        }


        private static IRuleElement createVariable(RuleElementData d, RuleElementView v) {
            return v.RuleElementType switch
            {
                RuleElementType.Boolean => boolValue(d, v),
                RuleElementType.String => strValue(d, v),
                RuleElementType.Integer => intValue(d, v),
                RuleElementType.Decimal => decimalValue(d, v),
                RuleElementType.Double => doubleValue(d, v),
                RuleElementType.DateTime => dateTimeValue(d, v),
                RuleElementType.Quantity => quantityValue(d, v),
                RuleElementType.Money => moneyValue(d, v),
                _ => ruleError(d, v)
            };
        }
        private static IRuleElement ruleError(RuleElementData d, RuleElementView v) {
            d.Value = v.StringValue;
            return new RuleError(d);
        }
        private static IRuleElement moneyValue(RuleElementData d, RuleElementView v) {
            d.Value = Variable.ToString(v.DecimalValue);
            d.UnitOrCurrencyId = v.CurrencyId;
            return new MoneyVariable(d);
        }
        private static IRuleElement quantityValue(RuleElementData d, RuleElementView v) {
            d.Value = Variable.ToString(v.DoubleValue);
            d.UnitOrCurrencyId = v.UnitId;
            return new QuantityVariable(d);
        }
        private static IRuleElement dateTimeValue(RuleElementData d, RuleElementView v) {
            d.Value = Variable.ToString(v.DateTimeValue);
            return new DateTimeVariable(d);
        }
        private static IRuleElement doubleValue(RuleElementData d, RuleElementView v) {
            d.Value = Variable.ToString(v.DoubleValue);
            return new DoubleVariable(d);
        }
        private static IRuleElement decimalValue(RuleElementData d, RuleElementView v) {
            d.Value = Variable.ToString(v.DecimalValue);
            return new DecimalVariable(d);
        }
        private static IRuleElement intValue(RuleElementData d, RuleElementView v) {
            d.Value = Variable.ToString(v.IntegerValue);
            return new IntegerVariable(d);
        }
        private static IRuleElement strValue(RuleElementData d, RuleElementView v) {
            d.Value = v.StringValue;
            return new StringVariable(d);
        }
        private static IRuleElement boolValue(RuleElementData d, RuleElementView v) {
            d.Value = Variable.ToString(v.BooleanValue);
            return new BooleanVariable(d);
        }

    }

}