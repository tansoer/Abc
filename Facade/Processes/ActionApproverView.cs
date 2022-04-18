using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class ActionApproverView :EntityBaseView {
        [DisplayName("Action")] public string ActionId { get; set; }
        [DisplayName("Approver Signature")] public string ApproverSignatureId { get; set; }
    }
}