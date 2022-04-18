using System;
using Abc.Aids.Calculator;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Values;
using Abc.Data.Rules;
using Abc.Domain.Common;

namespace Abc.Domain.Rules {

    public abstract class BaseVariable<TVariable, TValue> : BaseOperand, IVariable<TValue>
        where TVariable : IVariable<TValue>
        where TValue : IComparable {
        private string operation;
        private object value;
        protected BaseVariable(RuleElementData d) : base(d) { }
        public TValue Value => toValue(Data?.Value);
        protected abstract TValue toValue(string v);
        protected internal string operationName(IVariable x, IVariable y, string op) {
            if (y is null) return operationName(x, op);
            operation = op;
            value = $"{x},{y}";
            x ??= new UnspecifiedVariable();
            y ??= new UnspecifiedVariable();
            return $"(({Name}={Value}) {op} ({x?.Name}={x?.GetValue()},{y?.Name}={y?.GetValue()}))";
        }
        protected internal string operationName(IVariable v, string op) {
            operation = op;
            value = v;
            v ??= new UnspecifiedVariable();
            return $"(({Name}={Value}) {op} ({v.Name}={v.GetValue()}))";
        }
        protected internal string operationName(string op) {
            operation = op;
            value = null;
            return $"({op} ({Name}={Value}))";
        }
        protected internal IVariable variable(string n, object v) {
            try {
                var x = createVariable(n, v);
                return x;
            }
            catch (Exception e) {
                return new RuleError(e.Message);
            }
            //=> Safe.Run(() => createVariable(n, v), newRuleError());
        }
        protected internal virtual IVariable createVariable(string n, object v) => VariableFactory.Create(n, v);
        protected internal IVariable newRuleError(string opName = null) {
            var d = new RuleElementData {
                RuleId = Data?.RuleId ?? Unspecified.String,
                RuleElementType = RuleElementType.Error,
                Name = Name,
                Code = opName ?? operation,
                Details = Variable.ToString(value),
                Value = Variable.ToString(Value)
            };
            return new RuleError(d);
        }
        protected internal virtual IVariable variable(IVariable x, IVariable y, string n, Func<object, object, object, object> f) => variable(
            operationName(x, y, n), f(Value, x?.GetValue(), y?.GetValue()));
        protected internal virtual IVariable variable(IVariable v, string n, Func<object, object, object> f) => variable(
            operationName(v, n), f(Value, v?.GetValue()));
        protected internal virtual IVariable variable(string n, Func<object, object> f) => variable(
            operationName(n), f(Value));
        public virtual IVariable IsEqual(IVariable v) => variable(v, "==", Compute.IsEqual);
        public virtual IVariable IsGreater(IVariable v) => variable(v, ">", Compute.IsGreater);
        public virtual IVariable IsLess(IVariable v) => variable(v, "<", Compute.IsLess);
        public object GetValue() => Value;
        public virtual IVariable And(IVariable v) => newRuleError(nameof(And));
        public virtual IVariable Or(IVariable v) => newRuleError(nameof(Or));
        public virtual IVariable Xor(IVariable v) => newRuleError(nameof(Xor));
        public virtual IVariable Not() => newRuleError(nameof(Not));
        public virtual IVariable Add(IVariable v) => newRuleError(nameof(Add));
        public virtual IVariable Multiply(IVariable v) => newRuleError(nameof(Multiply));
        public virtual IVariable Divide(IVariable v) => newRuleError(nameof(Divide));
        public virtual IVariable Subtract(IVariable v) => newRuleError(nameof(Subtract));
        public virtual IVariable Power(IVariable v) => newRuleError(nameof(Power));
        public virtual IVariable Inverse() => newRuleError(nameof(Inverse));
        public virtual IVariable Opposite() => newRuleError(nameof(Opposite));
        public virtual IVariable Square() => newRuleError(nameof(Square));
        public virtual IVariable Sqrt() => newRuleError(nameof(Sqrt));
        public virtual IVariable GetSecond() => newRuleError(nameof(GetSecond));
        public virtual IVariable GetMinute() => newRuleError(nameof(GetMinute));
        public virtual IVariable GetHour() => newRuleError(nameof(GetHour));
        public virtual IVariable GetDay() => newRuleError(nameof(GetDay));
        public virtual IVariable GetMonth() => newRuleError(nameof(GetMonth));
        public virtual IVariable GetYear() => newRuleError(nameof(GetYear));
        public virtual IVariable GetInterval(IVariable v) => newRuleError(nameof(GetInterval));
        public virtual IVariable GetAge() => newRuleError(nameof(GetAge));
        public virtual IVariable ToSeconds() => newRuleError(nameof(ToSeconds));
        public virtual IVariable ToMinutes() => newRuleError(nameof(ToMinutes));
        public virtual IVariable ToHours() => newRuleError(nameof(ToHours));
        public virtual IVariable ToDays() => newRuleError(nameof(ToDays));
        public virtual IVariable ToMonths() => newRuleError(nameof(ToMonths));
        public virtual IVariable ToYears() => newRuleError(nameof(ToYears));
        public virtual IVariable AddSeconds(IVariable v) => newRuleError(nameof(AddSeconds));
        public virtual IVariable AddMinutes(IVariable v) => newRuleError(nameof(AddMinutes));
        public virtual IVariable AddHours(IVariable v) => newRuleError(nameof(AddHours));
        public virtual IVariable AddDays(IVariable v) => newRuleError(nameof(AddDays));
        public virtual IVariable AddMonths(IVariable v) => newRuleError(nameof(AddMonths));
        public virtual IVariable AddYears(IVariable v) => newRuleError(nameof(AddYears));
        public virtual IVariable GetLength() => newRuleError(nameof(GetLength));
        public virtual IVariable ToUpper() => newRuleError(nameof(ToUpper));
        public virtual IVariable ToLower() => newRuleError(nameof(ToLower));
        public virtual IVariable Trim() => newRuleError(nameof(Trim));
        public virtual IVariable Substring(IVariable x, IVariable y = null) => newRuleError(nameof(Substring));
        public virtual IVariable Contains(IVariable v) => newRuleError(nameof(Contains));
        public virtual IVariable EndsWith(IVariable v) => newRuleError(nameof(EndsWith));
        public virtual IVariable StartsWith(IVariable v) => newRuleError(nameof(StartsWith));
        public IVariable Calculate(Operation op, IVariable a = null, IVariable b = null) {
            return op switch {
                Operation.Dummy => this,
                Operation.Add => Add(a),
                Operation.Subtract => Subtract(a),
                Operation.Multiply => Multiply(a),
                Operation.Divide => Divide(a),
                Operation.Power => Power(a),
                Operation.Inverse => Inverse(),
                Operation.Opposite => Opposite(),
                Operation.Square => Square(),
                Operation.Sqrt => Sqrt(),
                Operation.And => And(a),
                Operation.Or => Or(a),
                Operation.Xor => Xor(a),
                Operation.Not => Not(),
                Operation.IsEqual => IsEqual(a),
                Operation.IsGreater => IsGreater(a),
                Operation.IsLess => IsLess(a),
                Operation.GetYear => GetYear(),
                Operation.GetMonth => GetMonth(),
                Operation.GetDay => GetDay(),
                Operation.GetHour => GetHour(),
                Operation.GetMinute => GetMinute(),
                Operation.GetSecond => GetSecond(),
                Operation.GetAge => GetAge(),
                Operation.GetInterval => GetInterval(a),
                Operation.ToYears => ToYears(),
                Operation.ToMonths => ToMonths(),
                Operation.ToDays => ToDays(),
                Operation.ToHours => ToHours(),
                Operation.ToMinutes => ToMinutes(),
                Operation.ToSeconds => ToSeconds(),
                Operation.AddSeconds => AddSeconds(a),
                Operation.AddMinutes => AddMinutes(a),
                Operation.AddHours => AddHours(a),
                Operation.AddDays => AddDays(a),
                Operation.AddMonths => AddMonths(a),
                Operation.AddYears => AddYears(a),
                Operation.GetLength => GetLength(),
                Operation.Substring => Substring(a, b),
                Operation.Contains => Contains(a),
                Operation.EndsWith => EndsWith(a),
                Operation.StartsWith => StartsWith(a),
                Operation.ToUpper => ToUpper(),
                Operation.ToLower => ToLower(),
                Operation.Trim => Trim(),
                _ => new RuleError(op.ToString()),
            };
        }
    }
}