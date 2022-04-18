using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class ThreadTypeView :ProcessElementTypeView {
        [DisplayName("Process type")] public string ProcessTypeId { get; set; }
    }
}