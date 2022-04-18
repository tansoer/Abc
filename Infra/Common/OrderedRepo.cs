using Abc.Aids.Methods;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Common {
    public abstract class OrderedRepo<TDomain, TData> :FilteredRepo<TDomain, TData>, IRepoOrdered
        where TDomain : IBaseEntity<TData> where TData : BaseData, new() {
        public virtual string SortOrder { get; set; }
        public string DescendingString => "_desc";
        protected OrderedRepo(DbContext c, DbSet<TData> s) :base(c, s) { }
        protected internal override IQueryable<TData> createSql() => addOrderBy(base.createSql());
        protected internal IQueryable<TData> addOrderBy(IQueryable<TData> q) => addOrderBy(q, getExpression());
        internal IQueryable<TData> addOrderBy(IQueryable<TData> q, Expression<Func<TData, object>> e)
            => Safe.Run(() => isDescending() ? q?.OrderByDescending(e) : q?.OrderBy(e), q);
        internal Expression<Func<TData, object>> getExpression() => getExpression(getProperty());
        internal static Expression<Func<TData, object>> getExpression(PropertyInfo p) {
            if (p is null) return null;
            var param = Expression.Parameter(typeof(TData), "x");
            var property = Expression.Property(param, p);
            var body = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<TData, object>>(body, param);
        }
        internal PropertyInfo getProperty() => typeof(TData).GetProperty(getName());
        internal string getName() => SortOrder?.Replace(DescendingString, "")?? "";
        internal bool isDescending() => SortOrder?.EndsWith(DescendingString) ?? false;
    }
}