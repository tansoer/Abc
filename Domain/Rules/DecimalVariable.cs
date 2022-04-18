using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public sealed class DecimalVariable : NumericalVariable<DecimalVariable, decimal> {

        public DecimalVariable(RuleElementData d = null) : base(d) { }
        public DecimalVariable(string name, decimal value, string ruleId = null, bool isContextId = false)
            : base(new RuleElementData {
                Name = name,
                Value = Variable.ToString(value),
                UnitOrCurrencyId = null,
                RuleId = isContextId ? null : ruleId,
                RuleContextId = isContextId ? ruleId : null,
                RuleElementType = RuleElementType.Decimal
            }) { }

        protected override decimal toValue(string v) => Variable.TryParse<decimal>(v);

    }

}