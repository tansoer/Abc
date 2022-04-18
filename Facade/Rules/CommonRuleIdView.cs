using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Rules {

    public abstract class CommonRuleIdView : EntityBaseView {

        [DisplayName("Rule")]
        public string RuleId { get; set; }

    }

}
