using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abc.Domain.Common {
    public interface IRepo<T> :
        IBaseRepo<T>,
        IRepoOrdered,
        IRepoFiltered<T>,
        IPagedRepo {
    }

    public interface IBaseRepo<T> {
        string ErrorMessage { get; }
        bool BeginTransaction();
        bool RollbackTransaction();
        bool CommitTransaction();
        bool SaveChanges();
        Task<bool> SaveChangesAsync();
        Task<List<T>> GetAsync();
        List<T> Get();
        Task<T> GetAsync(string id);
        T Get(string id);
        Task<bool> DeleteAsync(string id);
        bool Delete(string id);
        Task<bool> AddAsync(T obj);
        bool Add(T obj);
        Task<bool> UpdateAsync(T obj);
        bool Update(T obj);
    }

    public interface IPagedRepo {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int TotalPages { get; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
    }

    public interface IRepoFiltered<T> {
        string SearchString { get; set; }
        string CurrentFilter { get; set; }
        string FixedFilter { get; set; }
        string FixedValue { get; set; }
        string[] FixedValues { get; set; }
        List<T> Get(string propertyName, string propertyValue);
    }

    public interface IRepoOrdered {
        string SortOrder { get; set; }
    }

}
