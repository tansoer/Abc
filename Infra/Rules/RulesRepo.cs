using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Infra.Common;

namespace Abc.Infra.Rules {

    public sealed class RulesRepo : PagedRepo<BaseRule, RuleData>,
        IRulesRepo {
        public RulesRepo(RuleDb c = null) : base(c, c?.Rules) { }
        protected internal override BaseRule toDomainObject(RuleData d) {
            if (d is null) return new UnspecifiedRule();
            if (d.RuleKind == RuleKind.ActivityRule) return new ActivityRule(d);
            return new Rule(d);
        }
    }
}

