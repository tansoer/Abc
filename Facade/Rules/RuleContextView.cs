using System.ComponentModel;

namespace Abc.Facade.Rules {

    public sealed class RuleContextView : CommonSetIdView {

        [DisplayName("Rule")]
        public string RuleId { get; set; }

    }

}
