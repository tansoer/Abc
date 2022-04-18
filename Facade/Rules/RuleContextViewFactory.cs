using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Common;

namespace Abc.Facade.Rules {

    public sealed class RuleContextViewFactory :
        AbstractViewFactory<RuleContextData, RuleContext, RuleContextView> {
        protected internal override RuleContext toObject(RuleContextData d) => new RuleContext(d);
    }

}