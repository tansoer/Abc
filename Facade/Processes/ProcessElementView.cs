using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Processes {
    public abstract class ProcessElementView :ProcessElementBaseView{
        [DisplayName("Rule context")]
        public string RuleContextId { get; set; }
    }
}
