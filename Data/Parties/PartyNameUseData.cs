using Abc.Data.Common;

namespace Abc.Data.Parties {

    public sealed class PartyNameUseData : EntityBaseData {

        public string NameUseTypeId { get; set; }

        public string PartyNameId { get; set; }
    }
}