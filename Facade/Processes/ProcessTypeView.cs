using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class ProcessTypeView :EntityTypeView {
        [DisplayName("Rule Set")] public string RuleSetId { get; set; }

    }
}
