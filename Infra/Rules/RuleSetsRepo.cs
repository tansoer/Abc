using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Infra.Common;

namespace Abc.Infra.Rules {

    public sealed class RuleSetsRepo : PagedRepo<IRuleSet, RuleSetData>,
        IRuleSetsRepo {
        public RuleSetsRepo(RuleDb c = null) : base(c, c?.RuleSets) { }

        protected internal override IRuleSet toDomainObject(RuleSetData d)
            => new RuleSet(d);
    }
}

