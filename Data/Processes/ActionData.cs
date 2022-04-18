namespace Abc.Data.Processes {
    public sealed class ActionData :ProcessElementData {
        public string ActionTypeId { get; set; }
        public string TaskId { get; set; }
        public string InitiatorSignatureId { get; set; }
        public string ActionStatusClassifierId { get; set; }
    }
}
