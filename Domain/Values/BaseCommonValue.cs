using System;
using Abc.Aids.Calculator;
using Abc.Aids.Extensions;
using Abc.Aids.Values;
using Abc.Data.Common;
using Abc.Domain.Common;

namespace Abc.Domain.Values {
    public abstract class BaseCommonValue :Value<ValueData> {
        protected BaseCommonValue(ValueData d = null) : base(d) { }
    }
    public abstract class BaseCommonValue<TVariable, TValue> :BaseCommonValue, IValue<TValue>
        where TVariable : IValue<TValue>
        where TValue : IComparable {
        protected BaseCommonValue(ValueData d) : base(d) { }
        protected BaseCommonValue(string name, ValueData d) : base(d) => Name = name;
        public TValue Value => toValue(Data?.Value);
        protected abstract TValue toValue(string v);
        protected internal string operationName(IValue x, IValue y, string op)
            => (y is null)? operationName(x, op)
            : $"(({Value}) {op} ({x?.GetValue()},{y?.GetValue()}))";
        protected internal string operationName(IValue v, string op)
            => $"(({Value}) {op} ({v?.GetValue()}))";
        protected internal string operationName(string op) => $"({op} ({Value}))";
        protected internal IValue variable(string n, object v) {
            try {
                var x = createVariable(n, v);
                return x;
            } catch (Exception e) { return new ErrorValue(e.Message); }

            //=> Safe.Run(() => createVariable(n, v), newRuleError());
        }
        protected internal virtual IValue createVariable(string n, object v) => ValueFactory.Create(n, v);
        protected internal IValue errorValue(string opName = null) {
            var d = new ValueData {
                ValueType = Abc.Data.Common.ValueType.Error,
                Value = Variable.ToString(Value)
            };
            return new ErrorValue(d);
        }
        protected internal virtual IValue
            variable(IValue x, IValue y, string n, Func<object, object, object, object> f) => variable(
            operationName(x, y, n), f(Value, x?.GetValue(), y?.GetValue()));
        protected internal virtual IValue variable(IValue v, string n, Func<object, object, object> f) => variable(
            operationName(v, n), f(Value, v?.GetValue()));
        protected internal virtual IValue variable(string n, Func<object, object> f) => variable(
            operationName(n), f(Value));
        public virtual IValue IsEqual(IValue v) => variable(v, "==", Compute.IsEqual);
        public virtual IValue IsGreater(IValue v) => variable(v, ">", Compute.IsGreater);
        public virtual IValue IsLess(IValue v) => variable(v, "<", Compute.IsLess);
        public object GetValue() => Value;
        public string Name { get; }
        public virtual IValue And(IValue v) => errorValue(nameof(And));
        public virtual IValue Or(IValue v) => errorValue(nameof(Or));
        public virtual IValue Xor(IValue v) => errorValue(nameof(Xor));
        public virtual IValue Not() => errorValue(nameof(Not));
        public virtual IValue Add(IValue v) => errorValue(nameof(Add));
        public virtual IValue Multiply(IValue v) => errorValue(nameof(Multiply));
        public virtual IValue Divide(IValue v) => errorValue(nameof(Divide));
        public virtual IValue Subtract(IValue v) => errorValue(nameof(Subtract));
        public virtual IValue Power(IValue v) => errorValue(nameof(Power));
        public virtual IValue Inverse() => errorValue(nameof(Inverse));
        public virtual IValue Opposite() => errorValue(nameof(Opposite));
        public virtual IValue Square() => errorValue(nameof(Square));
        public virtual IValue Sqrt() => errorValue(nameof(Sqrt));
        public virtual IValue GetSecond() => errorValue(nameof(GetSecond));
        public virtual IValue GetMinute() => errorValue(nameof(GetMinute));
        public virtual IValue GetHour() => errorValue(nameof(GetHour));
        public virtual IValue GetDay() => errorValue(nameof(GetDay));
        public virtual IValue GetMonth() => errorValue(nameof(GetMonth));
        public virtual IValue GetYear() => errorValue(nameof(GetYear));
        public virtual IValue GetInterval(IValue v) => errorValue(nameof(GetInterval));
        public virtual IValue GetAge() => errorValue(nameof(GetAge));
        public virtual IValue ToSeconds() => errorValue(nameof(ToSeconds));
        public virtual IValue ToMinutes() => errorValue(nameof(ToMinutes));
        public virtual IValue ToHours() => errorValue(nameof(ToHours));
        public virtual IValue ToDays() => errorValue(nameof(ToDays));
        public virtual IValue ToMonths() => errorValue(nameof(ToMonths));
        public virtual IValue ToYears() => errorValue(nameof(ToYears));
        public virtual IValue AddSeconds(IValue v) => errorValue(nameof(AddSeconds));
        public virtual IValue AddMinutes(IValue v) => errorValue(nameof(AddMinutes));
        public virtual IValue AddHours(IValue v) => errorValue(nameof(AddHours));
        public virtual IValue AddDays(IValue v) => errorValue(nameof(AddDays));
        public virtual IValue AddMonths(IValue v) => errorValue(nameof(AddMonths));
        public virtual IValue AddYears(IValue v) => errorValue(nameof(AddYears));
        public virtual IValue GetLength() => errorValue(nameof(GetLength));
        public virtual IValue ToUpper() => errorValue(nameof(ToUpper));
        public virtual IValue ToLower() => errorValue(nameof(ToLower));
        public virtual IValue Trim() => errorValue(nameof(Trim));
        public virtual IValue Substring(IValue x, IValue y = null) => errorValue(nameof(Substring));
        public virtual IValue Contains(IValue v) => errorValue(nameof(Contains));
        public virtual IValue EndsWith(IValue v) => errorValue(nameof(EndsWith));
        public virtual IValue StartsWith(IValue v) => errorValue(nameof(StartsWith));
        public IValue Calculate(Operation op, IValue a = null, IValue b = null) => op switch {
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
            Operation.Substring => b == null ? Substring(a) : Substring(a, b),
            Operation.Contains => Contains(a),
            Operation.EndsWith => EndsWith(a),
            Operation.StartsWith => StartsWith(a),
            Operation.ToUpper => ToUpper(),
            Operation.ToLower => ToLower(),
            Operation.Trim => Trim(),
            _ => new ErrorValue(op.ToString()),
        };
    }
}