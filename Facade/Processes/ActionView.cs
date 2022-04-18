using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class ActionView :ProcessElementView{
        [DisplayName("Action type")]
        public string ActionTypeId { get; set; }
        [DisplayName("Task")]
        public string TaskId { get; set; }
        [DisplayName("Initiator signature")]
        public string InitiatorSignatureId { get; set; }
        [DisplayName("Action status classifier")]
        public string ActionStatusClassifierId { get; set; }
    }
}
