using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Rules {

    public sealed class RuleOverrideView : PartySignatureBaseView {

        [DisplayName("Rule Set")]
        public string RuleSetId { get; set; }

        [DisplayName("Rule")]
        public string RuleId { get; set; }

        [DisplayName("Context")]
        public string RuleContextId { get; set; }

        [DisplayName("Variable")]
        public string VariableId { get; set; }

    }

}
