namespace Abc.Data.Common {
    public interface IRelationshipData :IEntityBaseData {
        public string RelationshipTypeId { get; set; }
        public string ConsumerEntityId { get; set; }
        public string ProviderEntityId { get; set; }
    }
}