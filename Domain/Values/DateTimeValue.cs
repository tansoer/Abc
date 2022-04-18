using System;
using Abc.Aids.Calculator;
using Abc.Aids.Extensions;
using Abc.Aids.Values;
using Abc.Data.Common;

namespace Abc.Domain.Values {

    public sealed class DateTimeValue : BaseCommonValue<DateTimeValue, DateTime> {

        public DateTimeValue(ValueData d = null) : base(d) { }
        public DateTimeValue(string name, DateTime value)
            : base(name, new ValueData {
                Value = Variable.ToString(value),
                UnitOrCurrencyId = null,
                ValueType = Abc.Data.Common.ValueType.DateTime
            }) { }

        protected override DateTime toValue(string v) => Variable.TryParse<DateTime>(v);

        public override IValue Add(IValue v) => variable(v, "+", Compute.Add);

        public override IValue Subtract(IValue v) => variable(v, "-", Compute.Subtract);

        public override IValue GetSecond() => variable("s", Compute.GetSecond);

        public override IValue GetMinute() => variable("m", Compute.GetMinute);

        public override IValue GetHour() => variable("h", Compute.GetHour);

        public override IValue GetDay() => variable("d", Compute.GetDay);

        public override IValue GetMonth() => variable("M", Compute.GetMonth);

        public override IValue GetYear() => variable("y", Compute.GetYear);

        public override IValue GetInterval(IValue v) => variable(v, "←→", Compute.GetInterval);

        public override IValue GetAge() => variable("a", Compute.GetAge);

        public override IValue ToSeconds() => variable("→s", Compute.ToSeconds);

        public override IValue ToMinutes() => variable("→m", Compute.ToMinutes);

        public override IValue ToHours() => variable("→h", Compute.ToHours);

        public override IValue ToDays() => variable("→d", Compute.ToDays);

        public override IValue ToMonths() => variable("→M", Compute.ToMonths);

        public override IValue ToYears() => variable("→y", Compute.ToYears);

        public override IValue AddSeconds(IValue v) => variable(v, "+s", Compute.AddSeconds);

        public override IValue AddMinutes(IValue v) => variable(v, "+m", Compute.AddMinutes);

        public override IValue AddHours(IValue v) => variable(v, "+h", Compute.AddHours);

        public override IValue AddDays(IValue v) => variable(v, "+d", Compute.AddDays);

        public override IValue AddMonths(IValue v) => variable(v, "+M", Compute.AddMonths);

        public override IValue AddYears(IValue v) => variable(v, "+y", Compute.AddYears);

    }

}