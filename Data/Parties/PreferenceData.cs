using Abc.Data.Roles;

namespace Abc.Data.Parties {

    public sealed class PreferenceData : PartyAttributeData, IPartyRoleAttributeData {
        public string UnitId { get; set; }
        public string PreferenceOptionId { get; set; }
        public string PreferenceTypeId { get; set; }
        public string PartyRoleId { get; set; }
        public double Weight { get; set; }
    }
}