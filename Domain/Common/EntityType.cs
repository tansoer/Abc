using Abc.Data.Common;

namespace Abc.Domain.Common {
    public abstract class EntityType<TRepo, TDomain, TData>: Entity<TData> 
        where TData: EntityTypeData, new()
        where TDomain: IEntity<TData>
        where TRepo: IRepo<TDomain>{
        protected EntityType() : this(null) { }
        protected EntityType(TData d) : base(d) { }
        public virtual TDomain BaseType => item<TRepo, TDomain>(baseTypeId); 
        internal string baseTypeId => get(Data?.BaseTypeId);
    }
}
