namespace Abc.Data.Parties {

    public abstract class PartySignatureBaseData : PartyAttributeData {

        public string PartyAuthenticationId { get; set; }
        public string PartySummaryId { get; set; }
        public string SignedEntityId { get; set; }
        public string SignedEntityTypeId { get; set; }

    }

}