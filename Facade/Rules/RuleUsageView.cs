using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Rules {

    public sealed class RuleUsageView : EntityBaseView {
        [DisplayName("Rule")] public string RuleId { get; set; }
        [DisplayName("Rule Set")] public string RuleSetId { get; set; }
    }
}