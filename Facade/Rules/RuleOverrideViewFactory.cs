using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Common;

namespace Abc.Facade.Rules {

    public sealed class RuleOverrideViewFactory :
        AbstractViewFactory<RuleOverrideData, RuleOverride, RuleOverrideView> {
        protected internal override RuleOverride toObject(RuleOverrideData d)
            => new RuleOverride(d);
    }
}
