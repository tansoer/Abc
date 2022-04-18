using Abc.Aids.Calculator;
using Abc.Aids.Enums;
using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public sealed class Operator : RuleElement {

        public Operator(RuleElementData d = null) : base(d) { }
        public Operator(Operation o, string ruleId, int index = 0) : base(
            new RuleElementData {
                Operation = o,
                Index = index,
                RuleId = ruleId,
                RuleElementType = RuleElementType.Operator,
            }) { }
        public Operator(int idx, Operation o, string ruleId) : base(
            new RuleElementData {
                Index = idx,
                Operation = o,
                Name = o.ToString(),
                RuleId = ruleId,
                RuleElementType = RuleElementType.Operator,
            }) { }

        public Operation Operation => Data?.Operation ?? Operation.Dummy;

        public override string Name => Operation.ToString();

    }

}
