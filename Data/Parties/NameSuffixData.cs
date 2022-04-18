using System.ComponentModel.DataAnnotations;
namespace Abc.Data.Parties {
    public sealed class NameSuffixData : NameAttributeData {
        [Required] public string SuffixTypeId { get; set; }
    }
}
