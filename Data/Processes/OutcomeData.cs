namespace Abc.Data.Processes {
    public sealed class OutcomeData :ProcessElementData {
        public string OutcomeTypeId { get; set; }
        public string ActionId { get; set; }
        public bool IsPossibleOutcome { get; set; }
    }
}
