using Abc.Aids.Enums;

namespace Abc.Data.Parties {

    public sealed class PartyNameData : PartyAttributeData {
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string PreferredName { get; set; }
        public NameType NameType { get; set; }
        public PartyType PartyType { get; set; }
    }
}
