using Abc.Aids.Enums;
using Abc.Data.Common;

namespace Abc.Data.Parties {
    public sealed class PartyData : EntityBaseData {
        public IsoGender Gender { get; set; }
        public string OrganizationTypeId { get; set; }
        public PartyType PartyType { get; set; }
    }
}
