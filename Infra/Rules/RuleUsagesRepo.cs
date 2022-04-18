using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Infra.Common;

namespace Abc.Infra.Rules {
    public sealed class RuleUsagesRepo : EntityRepo<RuleUsage, RuleUsageData>, IRuleUsagesRepo {
        public RuleUsagesRepo(RuleDb c = null) : base(c, c?.RuleUsages) { }
    }
}

