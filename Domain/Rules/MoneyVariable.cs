using System;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Currencies;

namespace Abc.Domain.Rules {

    public sealed class MoneyVariable : ComparableVariable<MoneyVariable, Money> {

        public MoneyVariable(RuleElementData d = null) : base(d) { }
        public MoneyVariable(string name, Money value, string ruleId = null, bool isContextId = false)
            : base(new RuleElementData {
                Name = name,
                Value = Variable.ToString(value?.Amount),
                UnitOrCurrencyId = value?.Currency?.Id,
                RuleId = isContextId ? null : ruleId,
                RuleContextId = isContextId ? ruleId : null,
                RuleElementType = RuleElementType.Money,
                ValidFrom = value?.ValidFrom,
                ValidTo = value?.ValidTo
            }) { }

        public string CurrencyId => Data?.UnitOrCurrencyId ?? Unspecified.String;

        protected override Money toValue(string v) {

            var amount = Variable.TryParse<decimal>(v);
            var c = new GetFrom<ICurrencyRepo, Currency>().ById(CurrencyId);
            return new Money(amount, c, ValidFrom);
        }
        protected internal override IVariable variable(IVariable v, string n, Func<object, object, object> f)
              => VariableFactory.Create(operationName(v, n), f(Value, v.GetValue()));

        protected internal override IVariable variable(string n, Func<object, object> f) {
            var d = variable(operationName(n), f(Value.Amount));
            if (d is DecimalVariable x)
                return VariableFactory.Create(x.Name, new Money(x.Value, Value.Currency));

            return d;
        }

        public override IVariable IsEqual(IVariable v) => variable(v, "==", isEqual);
        public override IVariable IsGreater(IVariable v) => variable(v, ">", isGreater);
        public override IVariable IsLess(IVariable v) => variable(v, "<", isLess);
        public override IVariable Add(IVariable v) => variable(v, "+", add);
        public override IVariable Multiply(IVariable v) => variable(v, "*", multiply);
        public override IVariable Subtract(IVariable v) => variable(v, "-", subtract);
        public override IVariable Divide(IVariable v) => variable(v, ":", divide);

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