using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Common;

namespace Abc.Facade.Rules {

    public sealed class RuleViewFactory :
        AbstractViewFactory<RuleData, BaseRule, RuleView> {

        protected internal override BaseRule toObject(RuleData d)
            => (d.RuleKind == RuleKind.Rule) ? (new Rule(d) as BaseRule) : new ActivityRule(d);
    }

}