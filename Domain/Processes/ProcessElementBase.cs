using Abc.Data.Processes;
using Abc.Domain.Common;

namespace Abc.Domain.Processes {
    public abstract class ProcessElementBase<TRepo, TEntity, TData> : Entity<TData>, IProcessElementBase<TEntity>
        where TData : ProcessElementBaseData, new()
        where TEntity: IEntity<TData>
        where TRepo: IRepo<TEntity>{
        protected ProcessElementBase() : this(null) { }
        protected ProcessElementBase(TData d) : base(d) { }
        public virtual TEntity Next => item<TRepo, TEntity>(nextId);
        public virtual TEntity Previous => item<TRepo, TEntity>(prevId);
        internal string nextId => get(Data?.NextElementId);
        internal string prevId => get(Data?.PreviousElementId);
    }

    public interface IProcessElementBase<TEntity>: IProcessElementBase {
        public TEntity Next { get; }
        public TEntity Previous { get; }
    }
    public interface IProcessElementBase{
    }
}
