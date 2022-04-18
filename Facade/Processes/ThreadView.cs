using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class ThreadView :ProcessElementView {
        [DisplayName("Thread type")] public string ThreadTypeId { get; set; }
        [DisplayName("Process")] public string ProcessId { get; set; }
        [DisplayName("Terminator signature")] public string TerminatorSignatureId { get; set; }
    }
}