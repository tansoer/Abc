using Abc.Data.Rules;

namespace Abc.Domain.Rules {
    public sealed class Rule : BaseRule {
        public Rule() : this(null) { }
        public Rule(RuleData r) : base(r) { }
    }
}
