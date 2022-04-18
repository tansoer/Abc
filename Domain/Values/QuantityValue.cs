using System;
using Abc.Aids.Calculator;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using ValueType = Abc.Data.Common.ValueType;

namespace Abc.Domain.Values {

    public sealed class QuantityValue : BaseNumericalValue<QuantityValue, Quantity> {

        private readonly Quantity quantity;

        public QuantityValue(ValueData d = null) : base(d) { }
        public QuantityValue(string name, Quantity value)
            : base(name, new ValueData {
                Value = Variable.ToString(value?.Amount),
                UnitOrCurrencyId = value?.Unit?.Id,
                ValueType = ValueType.Quantity
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
        protected internal override IValue variable(IValue v, string n, Func<object, object, object> f)
              => v is DoubleValue ?
                  ValueFactory.Create(operationName(v, n), f(Value, (double) v.GetValue())) :
                  ValueFactory.Create(operationName(v, n), f(Value, v.GetValue()));

        protected internal override IValue variable(string n, Func<object, object> f)
            => ValueFactory.Create(operationName(n), f(Value));

        public override IValue IsEqual(IValue v) => variable(v, "==", isEqual);
        public override IValue IsGreater(IValue v) => variable(v, ">", isGreater);
        public override IValue IsLess(IValue v) => variable(v, "<", isLess);
        public override IValue Add(IValue v) => variable(v, "+", add);
        public override IValue Multiply(IValue v) => variable(v, "*", multiply);
        public override IValue Subtract(IValue v) => variable(v, "-", subtract);
        public override IValue Divide(IValue v) => variable(v, ":", divide);
        public override IValue Power(IValue v) => variable(v, "^", power);
        public override IValue Inverse() => variable("¹/ₓ", inverse);
        public override IValue Square() => variable("²", square);
        public override IValue Sqrt() => variable("√", sqrt);
        public override IValue Opposite() => variable("-", opposite);
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