using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Common;

namespace Abc.Facade.Rules {

    public sealed class RuleUsageViewFactory :
        AbstractViewFactory<RuleUsageData, RuleUsage, RuleUsageView> {

        protected internal override RuleUsage toObject(RuleUsageData d)
            => new RuleUsage(d);
    }
}
