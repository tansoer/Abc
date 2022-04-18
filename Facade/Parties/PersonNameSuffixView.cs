using System.ComponentModel;

namespace Abc.Facade.Parties {
    public sealed class PersonNameSuffixView :NameAttributeView {
        [DisplayName("Suffix")]
        public string SuffixTypeId { get; set; }
    }
}
