using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class OutcomeApproverView :EntityBaseView {
        [DisplayName("Outcome")] public string OutcomeId { get; set; }
        [DisplayName("Approver Signature")] public string ApproverSignatureId { get; set; }
    }
}