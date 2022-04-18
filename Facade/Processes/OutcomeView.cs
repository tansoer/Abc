using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class OutcomeView :ProcessElementView {
        [DisplayName("Outcome type")] public string OutcomeTypeId { get; set; }
        [DisplayName("Action")] public string ActionId { get; set; }
        [DisplayName("Is possible outcome")] public bool IsPossibleOutcome { get; set; }
    }
}