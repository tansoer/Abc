using Abc.Data.Common;

namespace Abc.Domain.Common {

    public abstract class RelationshipType<TData> : Entity<TData> where TData : RelationshipTypeData, new() {
        protected RelationshipType(TData d = null) : base(d) { }
        public string ConsumerTypeId => get(Data?.ConsumerTypeId);
        public string ProviderTypeId => get(Data?.ProviderTypeId);
    }
}