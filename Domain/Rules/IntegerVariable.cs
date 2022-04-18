using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public sealed class IntegerVariable : NumericalVariable<IntegerVariable, int> {

        public IntegerVariable(RuleElementData d = null) : base(d) { }
        public IntegerVariable(string name, int value, string ruleId = null, bool isContextId = false)
            : base(new RuleElementData {
                Name = name,
                Value = Variable.ToString(value),
                UnitOrCurrencyId = null,
                RuleId = isContextId ? null : ruleId,
                RuleContextId = isContextId ? ruleId : null,
                RuleElementType = RuleElementType.Integer
            }) { }

        protected override int toValue(string v) => Variable.TryParse<int>(v);

    }

}