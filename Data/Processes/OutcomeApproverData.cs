using Abc.Data.Common;

namespace Abc.Data.Processes {
    public sealed class OutcomeApproverData :EntityBaseData {
        public string OutcomeId { get; set; }
        public string ApproverSignatureId { get; set; }
    }
}
