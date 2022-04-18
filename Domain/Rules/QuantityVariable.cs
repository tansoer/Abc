using System;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Quantities;

namespace Abc.Domain.Rules {

    public sealed class QuantityVariable : NumericalVariable<QuantityVariable, Quantity> {

        private readonly Quantity quantity;

        public QuantityVariable(RuleElementData d = null) : base(d) { }
        public QuantityVariable(string name, Quantity value, string ruleId = null, bool isContextId = false)
            : base(new RuleElementData {
                Name = name,
                Value = Variable.ToString(value?.Amount),
                UnitOrCurrencyId = value?.Unit?.Id,
                RuleId = isContextId ? null : ruleId,
                RuleContextId = isContextId ? ruleId : null,
                RuleElementType = RuleElementType.Quantity
            }) {

            quantity = value;
        }

        public string UnitId => Data?.UnitOrCurrencyId ?? Unspecified.String;
        protected override Quantity toValue(string v) {
            var q = $"{v} {UnitId}";
            if (quantity?.ToString() == q) return quantity;
            var amount = Variable.TryParse<double>(v);
            var u = new GetFrom<IUnitsRepo, Unit>().ById(Data.UnitOrCurrencyId);

            return new Quantity(amount, u);
        }
        protected internal override IVariable variable(IVariable v, string n, Func<object, object, object> f)
              => v is DoubleVariable ?
                  VariableFactory.Create(operationName(v, n), f(Value, (double) v.GetValue())) :
                  VariableFactory.Create(operationName(v, n), f(Value, v.GetValue()));

        protected internal override IVariable variable(string n, Func<object, object> f)
            => VariableFactory.Create(operationName(n), f(Value));

        public override IVariable IsEqual(IVariable v) => variable(v, "==", isEqual);
        public override IVariable IsGreater(IVariable v) => variable(v, ">", isGreater);
        public override IVariable IsLess(IVariable v) => variable(v, "<", isLess);
        public override IVariable Add(IVariable v) => variable(v, "+", add);
        public override IVariable Multiply(IVariable v) => variable(v, "*", multiply);
        public override IVariable Subtract(IVariable v) => variable(v, "-", subtract);
        public override IVariable Divide(IVariable v) => variable(v, ":", divide);
        public override IVariable Power(IVariable v) => variable(v, "^", power);
        public override IVariable Inverse() => variable("¹/ₓ", inverse);
        public override IVariable Square() => variable("²", square);
        public override IVariable Sqrt() => variable("√", sqrt);
        public override IVariable Opposite() => variable("-", opposite);
        private static object opposite(object x)
            => Safe.Run(() => ((Quantity) x).Opposite(), Quantity.Unspecified);
        private static object sqrt(object x)
            => Safe.Run(() => ((Quantity) x).Sqrt(), Quantity.Unspecified);
        private static object square(object x)
            => Safe.Run(() => ((Quantity) x).Power(2), Quantity.Unspecified);
        private static object inverse(object x)
            => Safe.Run(() => ((Quantity) x).Inverse(), Quantity.Unspecified);
        private static object power(object x, object y)
            => Safe.Run(() => ((Quantity) x).Power((int) y), Quantity.Unspecified);
        private static object isEqual(object x, object y)
            => Safe.Run(() => ((Quantity) x).IsEqual((Quantity) y), false);
        private static object isGreater(object x, object y)
            => Safe.Run(() => ((Quantity) x).IsGreater((Quantity) y), false);
        private static object isLess(object x, object y)
            => Safe.Run(() => ((Quantity) x).IsLess((Quantity) y), false);
        private static object multiply(object x, object y)
            => Safe.Run(() => ((Quantity) x).Multiply((Quantity) y), Quantity.Unspecified);
        private static object divide(object x, object y)
            => Safe.Run(() => ((Quantity) x).Divide((Quantity) y), Quantity.Unspecified);
        private static object multiply(object x, double y)
            => Safe.Run(() => ((Quantity) x).Multiply(y), Quantity.Unspecified);
        private static object divide(object x, double y)
            => Safe.Run(() => ((Quantity) x).Divide(y), Quantity.Unspecified);
        private static object add(object x, object y)
            => Safe.Run(() => ((Quantity) x).Add((Quantity) y), Quantity.Unspecified);
        private static object subtract(object x, object y)
            => Safe.Run(() => ((Quantity) x).Subtract((Quantity) y), Quantity.Unspecified);
    }

}