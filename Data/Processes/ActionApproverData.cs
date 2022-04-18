using Abc.Data.Common;

namespace Abc.Data.Processes {
    public sealed class ActionApproverData :EntityBaseData {
        public string ActionId { get; set; }
        public string ApproverSignatureId { get; set; }
    }
}
