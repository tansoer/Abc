using System.ComponentModel.DataAnnotations;
namespace Abc.Data.Parties {
    public sealed class NamePrefixData : NameAttributeData {
        [Required] public string PrefixTypeId { get; set; }
    }
}
