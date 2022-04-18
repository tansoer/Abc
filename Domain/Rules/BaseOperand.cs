using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public abstract class BaseOperand : RuleElement {

        protected BaseOperand() : this(null) { }
        protected BaseOperand(RuleElementData d) : base(d) { }

    }

}