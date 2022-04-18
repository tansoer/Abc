using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Values;
using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public sealed class BooleanVariable : BaseVariable<BooleanVariable, bool> {

        public BooleanVariable(RuleElementData d = null) : base(d) { }
        public BooleanVariable(string name, bool value, string ruleId = null, bool isContextId = false)
            : base(new RuleElementData {
                Name = name,
                Value = Variable.ToString(value),
                UnitOrCurrencyId = null,
                RuleId = isContextId ? null : ruleId,
                RuleContextId = isContextId ? ruleId : null,
                RuleElementType = RuleElementType.Boolean
            }) { }

        protected override bool toValue(string v) => Variable.TryParse<bool>(v);

        public override IVariable Not() => variable("!", Compute.Not);

        public override IVariable And(IVariable v) => variable(v, "&", Compute.And);

        public override IVariable Or(IVariable v) => variable(v, "|", Compute.Or);

        public override IVariable Xor(IVariable v) => variable(v, "^", Compute.Xor);

        public override IVariable Add(IVariable v) => variable(v, "+", Compute.Add);

        public override IVariable Multiply(IVariable v) => variable(v, "*", Compute.Multiply);

    }

}