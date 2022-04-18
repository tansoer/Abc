using Abc.Data.Common;
namespace Abc.Domain.Common {
    public abstract class Relationship<TData> : Entity<TData> where TData : RelationshipData, new() {
        protected Relationship() : this(null) { }
        protected Relationship(TData d) : base(d) { }
        public string RelationshipTypeId => get(Data?.RelationshipTypeId);
        public string ConsumerEntityId => get(Data?.ConsumerEntityId);
        public string ProviderEntityId => get(Data?.ProviderEntityId);
    }
}