using System;
using System.Linq;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Common {
    public abstract class PagedRepo<TDomain, TData>: OrderedRepo<TDomain, TData>, IPagedRepo
        where TDomain : IBaseEntity<TData> where TData : BaseData, new() {
        public int PageIndex { get; set; }
        public int TotalPages => totalPages;
        public bool HasNextPage => PageIndex < TotalPages;
        public bool HasPreviousPage => PageIndex > 1;
        public int PageSize { get; set; } = Constants.DefaultPageSize;
        protected PagedRepo(DbContext c, DbSet<TData> s) : base(c, s) { }
        internal int totalPages => (int) Math.Ceiling(countPages);
        internal int totalItems => base.createSql().Count();
        internal double countPages => totalItems / (double)PageSize;
        internal int countItems => (PageIndex - 1) * PageSize;
        internal bool isNotPaged => PageIndex < 1;
        protected internal override IQueryable<TData> createSql() => addSkip(base.createSql());
        internal IQueryable<TData> addSkip(IQueryable<TData> q) => isNotPaged? q: q?.Skip(countItems).Take(PageSize);
    }
}