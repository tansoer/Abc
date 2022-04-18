using System;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Values;
using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public sealed class DateTimeVariable : BaseVariable<DateTimeVariable, DateTime> {

        public DateTimeVariable(RuleElementData d = null) : base(d) { }
        public DateTimeVariable(string name, DateTime value, string ruleId = null, bool isContextId = false)
            : base(new RuleElementData {
                Name = name,
                Value = Variable.ToString(value),
                UnitOrCurrencyId = null,
                RuleId = isContextId ? null : ruleId,
                RuleContextId = isContextId ? ruleId : null,
                RuleElementType = RuleElementType.DateTime
            }) { }

        protected override DateTime toValue(string v) => Variable.TryParse<DateTime>(v);

        public override IVariable Add(IVariable v) => variable(v, "+", Compute.Add);

        public override IVariable Subtract(IVariable v) => variable(v, "-", Compute.Subtract);

        public override IVariable GetSecond() => variable("s", Compute.GetSecond);

        public override IVariable GetMinute() => variable("m", Compute.GetMinute);

        public override IVariable GetHour() => variable("h", Compute.GetHour);

        public override IVariable GetDay() => variable("d", Compute.GetDay);

        public override IVariable GetMonth() => variable("M", Compute.GetMonth);

        public override IVariable GetYear() => variable("y", Compute.GetYear);

        public override IVariable GetInterval(IVariable v) => variable(v, "←→", Compute.GetInterval);

        public override IVariable GetAge() => variable("a", Compute.GetAge);

        public override IVariable ToSeconds() => variable("→s", Compute.ToSeconds);

        public override IVariable ToMinutes() => variable("→m", Compute.ToMinutes);

        public override IVariable ToHours() => variable("→h", Compute.ToHours);

        public override IVariable ToDays() => variable("→d", Compute.ToDays);

        public override IVariable ToMonths() => variable("→M", Compute.ToMonths);

        public override IVariable ToYears() => variable("→y", Compute.ToYears);

        public override IVariable AddSeconds(IVariable v) => variable(v, "+s", Compute.AddSeconds);

        public override IVariable AddMinutes(IVariable v) => variable(v, "+m", Compute.AddMinutes);

        public override IVariable AddHours(IVariable v) => variable(v, "+h", Compute.AddHours);

        public override IVariable AddDays(IVariable v) => variable(v, "+d", Compute.AddDays);

        public override IVariable AddMonths(IVariable v) => variable(v, "+M", Compute.AddMonths);

        public override IVariable AddYears(IVariable v) => variable(v, "+y", Compute.AddYears);

    }

}