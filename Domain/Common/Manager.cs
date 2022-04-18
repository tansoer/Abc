using Abc.Data.Common;
using System.Collections.Generic;

namespace Abc.Domain.Common {

    public interface IManagersRepo :IRepo<IManager> {

    }

    public interface IManager : IEntity<ManagerData> {

    }

    public interface IManager<TRepo, TItem> :IManager
        where TRepo : IRepo<TItem>
        where TItem : IEntity {
        public TRepo Repo { get; }
        public void Add(TItem p);
        public void Delete(TItem p);
        public void Update(TItem p);
        public TItem FindById(string id);
    }

    //TODO 1. Probably we have to add the whole query pattern here for the flexible search
    //2. OrderManagerData must be refactored later for general manager data
    public abstract class Manager<TRepo, TItem> : Entity<ManagerData>, IManager<TRepo, TItem>
        where TRepo : IRepo<TItem>
        where TItem : IEntity {
        protected Manager() : this(null) { }
        protected Manager(ManagerData d) : base (d) { }
        public TRepo Repo => GetRepo.Instance<TRepo>();
        public virtual void Add(TItem p) => Repo.Add(p);
        public virtual void Delete(TItem p) => Repo.Delete(p.Id);
        public virtual void Update(TItem p) => Repo.Update(p);
        public virtual TItem FindById(string id) => Repo.Get(id);
        //TODO has to be fixed ... can result in an enormous amount of items in the list
        protected internal IReadOnlyList<TObj> find<TR, TObj>(string searchString)
        where TR : IRepo<TObj> {
            var p = GetRepo.Instance<TR>();
            p.SearchString = searchString;
            var l = p.Get();
            return l.AsReadOnly();
        }
    }
}
