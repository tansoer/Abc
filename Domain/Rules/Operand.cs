using Abc.Aids.Enums;
using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public sealed class Operand : BaseOperand {

        public Operand(RuleElementData d = null) : base(d) { }
        public Operand(string name, string ruleId, int index = 0) : base(
            new RuleElementData {
                Name = name,
                Index = index,
                RuleId = ruleId,
                RuleElementType = RuleElementType.Operand,
            }) { }

        public Operand(int idx, string name, string ruleId, string definition) : base(
            new RuleElementData {
                Index = idx,
                Name = name,
                Details = definition,
                RuleId = ruleId,
                RuleElementType = RuleElementType.Operand,
            }) { }

    }

}
