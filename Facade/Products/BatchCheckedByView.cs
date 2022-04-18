using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class BatchCheckedByView : EntityBaseView {
        [DisplayName("Batch")] [Required] public string BatchId { get; set; }
        [DisplayName("Signature")] [Required] public string PartySignatureId { get; set; }
        [DisplayName("Checked By")] public string CheckedBy { get; set; }
        public string Address { get; set; }
        [DisplayName("Email")] public string EmailAddress { get; set; }
        [DisplayName("Phone")] public string PhoneNumber { get; set; }
    }
}
