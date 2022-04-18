using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Parties {
    public sealed class PersonEthnicityView: PartyAttributeView {
        [DisplayName("Ethnicity")] public string EthnicityId { get; set; }
    }
}
