using Abc.Facade.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abc.Facade.Parties {
    public abstract class NameAttributeView :PartyAttributeView {
        [DisplayName("Name")] [Required] public string NameId { get; set; }
        [Required] public byte Index { get; set; }
    }
}
