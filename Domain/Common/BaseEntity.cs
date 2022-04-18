using System;
using Abc.Data.Common;

namespace Abc.Domain.Common
{
    public abstract class BaseEntity<TData> :Value<TData>, IBaseEntity<TData>
        where TData : BaseData, new() {
        protected BaseEntity() : this(null) { }
        protected BaseEntity(TData d) : base(d) { }
        public virtual string Id => get(Data?.Id);
        public virtual DateTime ValidFrom => get(Data?.ValidFrom);
        public virtual DateTime ValidTo => get(Data?.ValidTo, false);
        public bool IsRemoved => !isNull(ValidTo, false);
        public override string ToString() => $"Id = {Id} ({base.ToString()})";
    }
}
