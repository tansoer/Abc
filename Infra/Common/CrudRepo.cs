using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Aids.Methods;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Common {
    public abstract class CrudRepo<TDomain, TData> :BaseRepo, IBaseRepo<TDomain>
        where TDomain : IBaseEntity<TData>
        where TData : BaseData, new() {
        protected internal DbSet<TData> dbSet;
        protected CrudRepo(DbContext c, DbSet<TData> s) :base(c) => dbSet = s;
        public List<TDomain> Get() => run(getList, listError);
        public TDomain Get(string id) => getItem(id, callingMethod());
        public async Task<List<TDomain>> GetAsync() => await runAsync(getListAsync, listError);
        public async Task<TDomain> GetAsync(string id) => await getItemAsync(id, callingMethod(), createUnspecified());
        internal async Task<TDomain> getItemAsync(string id, string method, TDomain u)
            => await runAsync(() => getAsync(id, method, u), x => onException(method, x, u));
        internal async Task<TDomain> getAsync(string id, string method, TDomain u) {
            if (id is null) return idIsNull(method, u);
            var d = await getDataAsync(id);
            if (d is null) return itemNotFound(method, id, u);
            var obj = toDomainObject(d);
            return obj;
        }
        private TDomain createUnspecified() => toDomainObject(new TData());
        public bool Delete(string id) => deleteItem(id, callingMethod());
        public bool Update(TDomain obj) => updateItem(obj, callingMethod());
        public bool Add(TDomain obj) => addItem(obj, callingMethod());
        internal async Task<List<TDomain>> getListAsync() {
            var query = createSql();
            var set = await runSqlQueryAsync(query);
            return toDomainObjectsList(set);
        }
        public async Task<bool> DeleteAsync(string id) => await deleteItemAsync(id, callingMethod());
        internal async Task<bool> deleteItemAsync(string id, string method)
            => await runAsync(() => deleteAsync(id, method), x => onException(method, x));
        internal async Task<bool> deleteAsync(string id, string method) {
            if (id is null) return onIdIsNull(method);
            var v = await getDataAsync(id);
            if (v is null) return onItemNotFound(id, method);
            dbSet?.Remove(v);
            return isTransactionOpen || await SaveChangesAsync();
        }
        public async Task<bool> AddAsync(TDomain obj) => await addItemAsync(obj, callingMethod());
        internal async Task<bool> addItemAsync(TDomain obj, string method)
            => await runAsync(() => addAsync(obj, method), x => onException(method, x));
        internal async Task<bool> addAsync(TDomain obj, string method) {
            if (obj is null) return onItemIsNull(method);
            var v = await getDataAsync(obj.Id);
            if (v is not null) return onItemFound(obj.Id, method);
            await dbSet.AddAsync(obj.Data);
            return isTransactionOpen || await SaveChangesAsync();
        }
        public async Task<bool> UpdateAsync(TDomain obj) => await updateItemAsync(obj, callingMethod());
        internal async Task<bool> updateItemAsync(TDomain obj, string method)
            => await runAsync(() => updateAsync(obj, method), x => onException(method, x));
        internal async Task<bool> updateAsync(TDomain obj, string method) {
            if (obj is null) return onItemIsNull(method);
            var d = await getDataAsync(obj.Id);
            if (d is null) return onItemNotFound(obj.Id, method);
            var from = obj.Data;
            Copy.Members(from, d, nameof(obj.Id));
            dbSet.Update(d);
            return isTransactionOpen || await SaveChangesAsync();
        }
        protected async Task<TData> getDataAsync(string id)
            => await dbSet.FirstOrDefaultAsync(m => m.Id == id);
        protected TData getData(string id) => dbSet.FirstOrDefault(m => m.Id == id);
        protected TData getDataById(TData d) => dbSet.Find(d.Id);
        internal List<TDomain> getList() {
            var query = createSql();
            var set = runSqlQuery(query);
            return toDomainObjectsList(set);
        }
        internal List<TDomain> listError(string msg) => reportError(msg, new List<TDomain>());
        internal TDomain itemError(string msg) => reportError(msg, createUnspecified());
        protected internal virtual IQueryable<TData> createSql() {
            var query = from s in dbSet select s;
            return query;
        }
        protected internal abstract TDomain toDomainObject(TData d);
        protected bool isInDatabase(TData d) => getDataById(d) != null;
        internal static async Task<List<TData>> runSqlQueryAsync(IQueryable<TData> query)
            => await query.AsNoTracking().ToListAsync();
        internal static List<TData> runSqlQuery(IQueryable<TData> query) => query.AsNoTracking().ToList();
        internal List<TDomain> toDomainObjectsList(List<TData> set)
            => set.Select(toDomainObject).ToList();
        private TDomain itemNotFound(string id, string methodName, TDomain obj)
            => reportError(string.Format(itemNotFoundMsg, id, methodName), obj);
        private TDomain onException(string methodName, string msg, TDomain obj)
            => reportError(string.Format(exceptionMsg, methodName, msg), obj);
        private TDomain idIsNull(string methodName, TDomain obj)
            => reportError(string.Format(idIsNullMsg, methodName), obj);
        internal bool updateItem(TDomain obj, string method)
            => run(() => update(obj, method), msg => onException(method, msg));
        internal bool deleteItem(string id, string method)
            => run(() => delete(id, method), msg => onException(method, msg));
        internal bool addItem(TDomain obj, string method)
            => run(() => add(obj, method), msg => onException(method, msg));
        internal bool delete(string id, string methodName) {
            if (id is null) return onIdIsNull(methodName);
            var v = getData(id);
            if (v is null) return onItemNotFound(id, methodName);
            dbSet.Remove(v);
            return isTransactionOpen || SaveChanges();
        }
        internal bool update(TDomain obj, string methodName) {
            if (obj is null) return onItemIsNull(methodName);
            var d = getData(obj.Id);
            if (d is null) return onItemNotFound(obj.Id, methodName);
            var from = obj.Data;
            Copy.Members(from, d, nameof(obj.Id));
            dbSet.Update(d);
            return isTransactionOpen || SaveChanges();
        }
        internal bool add(TDomain obj, string methodName) {
            if (obj is null) return onItemIsNull(methodName);
            var v = getData(obj.Id);
            if (v is not null) return onItemFound(obj.Id, methodName);
            dbSet.Add(obj.Data);
            return isTransactionOpen || SaveChanges();
        }
        internal TDomain get(string id, string method) {
            var u = createUnspecified();
            if (id is null) return idIsNull(method, u);
            var d = getData(id);
            if (d is null) return itemNotFound(method, id, u);
            var obj = toDomainObject(d);
            return obj;
        }
        internal TDomain getItem(string id, string method) => run(() => get(id, method), itemError);
    }
}