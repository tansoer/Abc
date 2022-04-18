using Abc.Data.Rules;
using Abc.Domain.Common;

namespace Abc.Domain.Rules {

    public sealed class RuleUsage : Entity<RuleUsageData> {

        public RuleUsage() : this(null) { }

        public RuleUsage(RuleUsageData d) : base(d) { }

        public string RuleId => get(Data?.RuleId);

        public string RuleSetId => get(Data?.RuleSetId);

        public BaseRule Rule => item<IRulesRepo, BaseRule>(RuleId);

        public IRuleSet RuleSet => item<IRuleSetsRepo, IRuleSet>(RuleSetId);
    }

}

