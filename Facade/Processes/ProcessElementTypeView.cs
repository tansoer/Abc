using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Processes {
    public abstract class ProcessElementTypeView :ProcessElementBaseView {
        [DisplayName("Rule set")] public string RuleSetId { get; set; }
        [DisplayName("Base type")] public string BaseTypeId { get; set; }
    }
}