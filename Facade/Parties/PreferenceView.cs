using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Parties {
    public sealed class PreferenceView :PartyAttributeView {
        [DisplayName("Unit")] public string UnitId { get; set; }
        [DisplayName("Option")] public string PreferenceOptionId { get; set; }
        [DisplayName("Preference")] public string PreferenceTypeId { get; set; }
        [DisplayName("Party Role")] public string PartyRoleId { get; set; }
        public double Weight { get; set; }
    }
}
