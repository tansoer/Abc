namespace Abc.Data.Common {
    public interface IRelationshipTypeData :IEntityTypeData {
        public string ConsumerTypeId { get; set; }
        public string ProviderTypeId { get; set; }
    }
}