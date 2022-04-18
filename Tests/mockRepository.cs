using System.Collections.Generic;
using System.Threading.Tasks;
using Abc.Aids.Extensions;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using System;

namespace Abc.Tests {

    internal abstract class mockRepo<TObj, TData>
        where TObj : IBaseEntity<TData>
        where TData : BaseData, new() {
        internal readonly List<TObj> list;
        protected mockRepo() {
            list = new List<TObj>();
        }
        public string ErrorMessage { get; set; }
        public bool BeginTransaction() => true;
        public bool RollbackTransaction() => true;
        public bool CommitTransaction() => true;
        public bool SaveChanges() => true;
        public async Task<bool> SaveChangesAsync() {
            await Task.CompletedTask;
            return SaveChanges();
        }
        public async Task<List<TObj>> GetAsync() {
            await Task.CompletedTask;
            return Get();
        }
        public List<TObj> Get() {
            if (FixedFilter is null) return list;
            var l = new List<TObj>();
            var p = typeof(TData).GetProperty(FixedFilter);
            foreach (var e in list) {
                var v = p?.GetValue(e.Data);
                if (v is null) continue;
                if (v.ToString() != Variable.Parse(FixedValue, p.PropertyType).ToString()) continue;
                l.Add(e);
            }
            return l;
        }
        public async Task<TObj> GetAsync(string id) {
            await Task.CompletedTask;
            return Get(id);
        }
        public TObj Get(string id) => list.Find(x => x.Data.Id == id);

        public async Task<bool> DeleteAsync(string id) {
            await Task.CompletedTask;
            return Delete(id);
        }
        public async Task<bool> AddAsync(TObj obj) {
            await Task.CompletedTask;
            return Add(obj);
        }
        public async Task<bool> UpdateAsync(TObj obj) {
            await Task.CompletedTask;
            return Update(obj);
        }
        public bool Delete(string id) {
            var obj = list.Find(x => x.Data.Id == id);
            list.Remove(obj);
            return true;
        }
        public bool Add(TObj obj) {
            list.Add(obj);
            return true;
        }
        public bool Update(TObj obj) {
            Delete(obj.Data.Id);
            list.Add(obj);
            return true;
        }
        public Unit GetByCode(string code) { throw new NotImplementedException();}
        public List<Unit> GetByCodes(List<string> codes) { throw new NotImplementedException();}
        public Task<Unit> GetByCodeAsync(string code) { throw new NotImplementedException(); }
        public Task<List<Unit>> GetByCodesAsync(List<string> codes) { throw new NotImplementedException(); }
        public List<TObj> Get(string propertyName, string propertyValue) {
            FixedFilter = propertyName;
            FixedValue = propertyValue;
            return Get();
        }
        public string SortOrder { get; set; }
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }
        public string FixedFilter { get; set; }
        public string FixedValue { get; set; }

        public string[] FixedValues { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}