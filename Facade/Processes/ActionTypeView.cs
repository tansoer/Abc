using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class ActionTypeView :ProcessElementTypeView {
        [DisplayName("Task type")] public string TaskTypeId { get; set; }
    }

}