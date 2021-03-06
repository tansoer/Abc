using System;
using Abc.Aids.Calculator;
using Abc.Aids.Methods;

namespace Abc.Domain.Rules {

    public sealed class VariableCalculator :Calculate {

        public override void Add() => setSafe(() => getPreLast().Add(getLast()));

        public override void AddDays() => setSafe(() => getPreLast().AddDays(getLast()));

        public override void AddHours() => setSafe(() => getPreLast().AddHours(getLast()));

        public override void AddMinutes() => setSafe(() => getPreLast().AddMinutes(getLast()));

        public override void AddMonths() => setSafe(() => getPreLast().AddMonths(getLast()));

        public override void AddSeconds() => setSafe(() => getPreLast().AddSeconds(getLast()));

        public override void AddYears() => setSafe(() => getPreLast().AddYears(getLast()));

        public override void And() => setSafe(() => getPreLast().And(getLast()));

        public override void Contains() => setSafe(() => getPreLast().Contains(getLast()));

        public override void Divide() => setSafe(() => getPreLast().Divide(getLast()));

        public override void EndsWith() => setSafe(() => getPreLast().EndsWith(getLast()));

        public override void IsEqual() => setSafe(() => getPreLast().IsEqual(getLast()));

        public override void GetAge() => setSafe(() => getLast().GetAge());

        public override void GetDay() => setSafe(() => getLast().GetDay());

        public override void GetHour() => setSafe(() => getLast().GetHour());

        public override void GetInterval() => setSafe(() => getPreLast().GetInterval(getLast()));

        public override void GetMinute() => setSafe(() => getLast().GetMinute());

        public override void GetMonth() => setSafe(() => getLast().GetMonth());

        public override void GetSecond() => setSafe(() => getLast().GetSecond());

        public override void GetYear() => setSafe(() => getLast().GetYear());

        public override void IsGreater() => setSafe(() => getPreLast().IsGreater(getLast()));

        public override void Inverse() => setSafe(() => getLast().Inverse());

        public override void GetLength() => setSafe(() => getLast().GetLength());

        public override void IsLess() => setSafe(() => getPreLast().IsLess(getLast()));

        public override void Multiply() => setSafe(() => getPreLast().Multiply(getLast()));

        public override void Not() => setSafe(() => getLast().Not());

        public override void Opposite() => setSafe(() => getLast().Opposite());

        public override void Or() => setSafe(() => getPreLast().Or(getLast()));

        public override void Power() => setSafe(() => getPreLast().Power(getLast()));

        public override void Sqrt() => setSafe(() => getLast().Sqrt());

        public override void Square() => setSafe(() => getLast().Square());

        public override void StartsWith() => setSafe(() => getPreLast().StartsWith(getLast()));

        public override void Substring() => setSafe(() => {
            var x = getLast();
            var y = getLast();

            return y is StringVariable ? y.Substring(x) : getLast().Substring(y, x);
        });

        public override void Subtract() => setSafe(() => getPreLast().Subtract(getLast()));

        public override void ToDays() => setSafe(() => getLast().ToDays());

        public override void ToHours() => setSafe(() => getLast().ToHours());

        public override void ToLower() => setSafe(() => getLast().ToLower());

        public override void ToMinutes() => setSafe(() => getLast().ToMinutes());

        public override void ToMonths() => setSafe(() => getLast().ToMonths());

        public override void ToSeconds() => setSafe(() => getLast().ToSeconds());

        public override void ToUpper() => setSafe(() => getLast().ToUpper());

        public override void ToYears() => setSafe(() => getLast().ToYears());

        public override void Trim() => setSafe(() => getLast().Trim());

        public override void Xor() => setSafe(() => getPreLast().Xor(getLast()));

        private IVariable getLast() => Get() as IVariable ?? new UnspecifiedVariable();

        private IVariable getPreLast() {
            var x = Get();
            var y = Get();
            Set(x);

            return y as IVariable ?? new UnspecifiedVariable();
        }

        private void setSafe(Func<IVariable> f) => Set(
            Safe.Run(f, new RuleError(nameof(f)), true)
        );

    }

}