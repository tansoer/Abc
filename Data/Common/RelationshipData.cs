namespace Abc.Data.Common {
    public abstract class RelationshipData : EntityBaseData, IRelationshipData {
        public string RelationshipTypeId { get; set; }
        public string ConsumerEntityId { get; set; }
        public string ProviderEntityId { get; set; }
    }
}