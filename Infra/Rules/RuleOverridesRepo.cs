using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Infra.Common;

namespace Abc.Infra.Rules {

    public sealed class RuleOverridesRepo : EntityRepo<RuleOverride, RuleOverrideData>,
        IRuleOverridesRepo {
        public RuleOverridesRepo(RuleDb c = null) : base(c, c?.RuleOverrides) { }
    }
}
