using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class OutcomeTypeView :ProcessElementTypeView {
        [DisplayName("Action type")] public string ActionTypeId { get; set; }
    }
}