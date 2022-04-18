using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Parties {
    public sealed class PersonNamePrefixView: NameAttributeView {
        [DisplayName("Prefix")]
        public string PrefixTypeId { get; set; }
    }
}
