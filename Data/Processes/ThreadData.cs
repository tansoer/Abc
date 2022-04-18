namespace Abc.Data.Processes {
    public sealed class ThreadData :ProcessElementData {
        public string ThreadTypeId { get; set; }
        public string ProcessId { get; set; }
        public string TerminatorSignatureId { get; set; }
    }
}
