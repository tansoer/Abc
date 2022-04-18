using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Common;

namespace Abc.Facade.Rules {

    public sealed class RuleSetViewFactory :
        AbstractViewFactory<RuleSetData, IRuleSet, RuleSetView> {
        protected internal override IRuleSet toObject(RuleSetData d)
            => new RuleSet(d);
    }
}
