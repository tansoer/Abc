using Abc.Data.Common;

namespace Abc.Domain.Common {
    public abstract class Entity<TData> :DetailedEntity<TData>, IEntity<TData>
        where TData : EntityBaseData, new() {
        protected Entity() : this(null) { }
        protected Entity(TData d) : base(d) { }
        public virtual string Name => get(Data?.Name);
        public virtual string Code => get(Data?.Code);
    }
}
