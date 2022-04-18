
using System.ComponentModel;

namespace Abc.Facade.Common {
    public abstract class PartySignatureBaseView : PartyAttributeView {
        [DisplayName("Party authentication")] public string PartyAuthenticationId { get; set; }
        [DisplayName("Party summary")] public string PartySummaryId { get; set; }
        [DisplayName("Signed entity")] public string SignedEntityId { get; set; }
        [DisplayName("Signed entity type")] public string SignedEntityTypeId { get; set; }
    }
}
