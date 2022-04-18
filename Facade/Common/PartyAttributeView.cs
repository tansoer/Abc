using System.ComponentModel;

namespace Abc.Facade.Common {
    public abstract class PartyAttributeView :EntityBaseView {
        [DisplayName("Party")] public string PartyId { get; set; }
    }
}
