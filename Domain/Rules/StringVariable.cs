using Abc.Aids.Enums;
using Abc.Aids.Values;
using Abc.Data.Rules;
using Abc.Domain.Common;

namespace Abc.Domain.Rules {

    public sealed class StringVariable : BaseVariable<StringVariable, string> {
        public StringVariable(RuleElementData d) : base(d) { }
        public StringVariable(string name, string value, string ruleId = null, bool isContextId = false)
            : base(new RuleElementData {
                Name = name,
                Value = value,
                UnitOrCurrencyId = null,
                RuleId = isContextId ? null : ruleId,
                RuleContextId = isContextId ? ruleId : null,
                RuleElementType = RuleElementType.String
            }) { }
        protected override string toValue(string v) => v ?? Unspecified.String;
        public override IVariable Add(IVariable v) => variable(v, "+", Compute.Add);
        public override IVariable GetLength() => variable("←→", Compute.GetLength);
        public override IVariable ToUpper() => variable("↑", Compute.ToUpper);
        public override IVariable ToLower() => variable("↓", Compute.ToLower);
        public override IVariable Trim() => variable("→←", Compute.Trim);
        public override IVariable Substring(IVariable x, IVariable y= null) => variable(x, y, "→←", Compute.Substring);
        public override IVariable Contains(IVariable v) => variable(v, "←→", Compute.Contains);
        public override IVariable EndsWith(IVariable v) => variable(v, "→", Compute.EndsWith);
        public override IVariable StartsWith(IVariable v) => variable(v, "←", Compute.StartsWith);

    }

}
