using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public sealed class DoubleVariable : NumericalVariable<DoubleVariable, double> {

        public DoubleVariable(RuleElementData d = null) : base(d) { }
        public DoubleVariable(string name, double value, string ruleId, bool isContextId = false)
            : base(new RuleElementData {
                Name = name,
                Value = Variable.ToString(value),
                UnitOrCurrencyId = null,
                RuleId = isContextId ? null : ruleId,
                RuleContextId = isContextId ? ruleId : null,
                RuleElementType = RuleElementType.Double
            }) { }
        public DoubleVariable(int idx, string name, double value, string ruleId, string definition, bool isContextId = false) : base(
            new RuleElementData {
                Index = idx,
                Name = name,
                Details = definition,
                Value = Variable.ToString(value),
                UnitOrCurrencyId = null,
                RuleId = isContextId ? null : ruleId,
                RuleContextId = isContextId ? ruleId : null,
                RuleElementType = RuleElementType.Double
            }) { }

        protected override double toValue(string v) => Variable.TryParse<double>(v);

    }

}
