using System;
using Abc.Aids.Calculator;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using DateTime = System.DateTime;
using ValueType = Abc.Data.Common.ValueType;

namespace Abc.Domain.Values {

    public sealed class MoneyValue : BaseComparableValue<MoneyValue, Money> {

        public MoneyValue(ValueData d = null) : base(d) { }
        public MoneyValue(string name, Money value)
            : base(name, new ValueData {
                Value = Variable.ToString(value?.Amount),
                UnitOrCurrencyId = value?.Currency?.Id,
                ValueType = ValueType.Money,
                ValidFrom = value?.ValidFrom
            }) { }

        public string CurrencyId => Data?.UnitOrCurrencyId ?? Unspecified.String;

        public DateTime From => Data?.ValidFrom ?? DateTime.Now;

        protected override Money toValue(string v) {

            var amount = Variable.TryParse<decimal>(v);
            var c = new GetFrom<ICurrencyRepo, Currency>().ById(CurrencyId);
            return new Money(amount, c, From);
        }
        protected internal override IValue variable(IValue v, string n, Func<object, object, object> f)
              => ValueFactory.Create(operationName(v, n), f(Value, v.GetValue()));

        protected internal override IValue variable(string n, Func<object, object> f) {
            var d = variable(operationName(n), f(Value.Amount));
            if (d is DecimalValue x)
                return ValueFactory.Create(x.Name, new Money(x.Value, Value.Currency));

            return d;
        }

        public override IValue IsEqual(IValue v) => variable(v, "==", isEqual);
        public override IValue IsGreater(IValue v) => variable(v, ">", isGreater);
        public override IValue IsLess(IValue v) => variable(v, "<", isLess);
        public override IValue Add(IValue v) => variable(v, "+", add);
        public override IValue Multiply(IValue v) => variable(v, "*", multiply);
        public override IValue Subtract(IValue v) => variable(v, "-", subtract);
        public override IValue Divide(IValue v) => variable(v, ":", divide);

        private static object isEqual(object x, object y)
            => Safe.Run(() => ((Money) x).IsEqual((Money) y), false);

        private static object isGreater(object x, object y)
            => Safe.Run(() => ((Money) x).IsGreater((Money) y), false);

        private static object isLess(object x, object y)
            => Safe.Run(() => ((Money) x).IsLess((Money) y), false);

        private static object multiply(object x, object y)
            => Safe.Run(() => ((Money) x).Multiply((decimal) y), Money.Unspecified);

        private static object divide(object x, object y)
            => Safe.Run(() => ((Money) x).Divide((decimal) y), Money.Unspecified);

        private static object add(object x, object y)
            => Safe.Run(() => ((Money) x).Add((Money) y), Money.Unspecified);

        private static object subtract(object x, object y)
            => Safe.Run(() => ((Money) x).Subtract((Money) y), Money.Unspecified);

    }

}