using Abc.Data.Common;

namespace Abc.Domain.Common
{
    public abstract class DetailedEntity<TData> : BaseEntity<TData>, IBaseEntity<TData>
        where TData : DetailedData, new()
    {
        protected DetailedEntity() : this(null) { }
        protected DetailedEntity(TData d) : base(d) { }
        public virtual string Details => get(Data?.Details);
    }
}
