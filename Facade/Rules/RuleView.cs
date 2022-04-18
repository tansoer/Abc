using System.ComponentModel;
using Abc.Data.Rules;

namespace Abc.Facade.Rules {

    public sealed class RuleView : CommonRuleIdView {

        [DisplayName("Rule Kind")]
        public RuleKind RuleKind { get; set; }

    }

}