namespace Abc.Data.Common {
    public abstract class RelationshipTypeData : EntityTypeData, IRelationshipTypeData {
        public string ConsumerTypeId { get; set; }
        public string ProviderTypeId { get; set; }
    }
}