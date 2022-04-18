using System.ComponentModel.DataAnnotations;
namespace Abc.Data.Parties {
    public abstract class NameAttributeData : PartyAttributeData {
        [Required] public string NameId { get; set; }
        [Required] public byte Index { get; set; }
    }
}
