using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Common {
    public abstract class FilteredRepo<TDomain, TData> : CrudRepo<TDomain, TData>, IRepoFiltered<TDomain>
        where TDomain : IBaseEntity<TData> where TData : BaseData, new() {
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }
        public string FixedFilter { get; set; }
        public string FixedValue { get; set; }
        public string[] FixedValues { get; set; }
        protected FilteredRepo(DbContext c, DbSet<TData> s) : base(c, s) { }
        protected internal override IQueryable<TData> createSql() {
            var q = base.createSql();
            q = addFilter(q);
            q = addSearch(q);
            return FixedValue is null ? q : addCustomFilter(q);
        }
        internal protected virtual IQueryable<TData> addCustomFilter(IQueryable<TData> q) => q;
        internal protected virtual IQueryable<TData> addFilter(IQueryable<TData> q) {
            var expression = fixedExpr();
            return expression is null ? q : q?.Where(expression);
        }
        internal protected virtual IQueryable<TData> addSearch(IQueryable<TData> q) {
            if (notSpecified(SearchString) ) return q;
            var expression = searchExpr();
            return q?.Where(expression);
        }
        internal static bool notSpecified(object o) => (o is string s)? string.IsNullOrEmpty(s): o is null;
        private bool notSpecified() => notSpecified(FixedFilter) || notSpecified(FixedValue);

        private List<Expression> getFilters()
            => FixedValues.Select(v => getExpressionConstant(v)).ToList();
        private Expression getFilter()
            => getExpressionConstant(FixedValues[0]);
        protected Expression getExpressionConstant(string fixedValue)
            => Expression.Constant(tryParse(fixedValue));
        private dynamic tryParse(string s)
            => Safe.Run(() => {
                Type t = typeof(TData)?.GetProperty(FixedFilter)?.PropertyType;
                return Variable.Parse(s, t);
            }, s);
        private Expression<Func<TData, bool>> fixedExpr() {
            if (notSpecified()) return null;
            var param = Expression.Parameter(typeof(TData), "s");
            var p = typeof(TData).GetProperty(FixedFilter);
            if (p is null) return null;
            Expression body = Expression.Property(param, p);
            if (FixedValues?.Length > 1) {
                var expressions = getFilters().Select(value => Expression.Equal(body, value)).ToList();
                for (int i = 1; i < expressions.Count; i++) {
                    body = i == 1 ? Expression.Or(expressions[0], expressions[1]) : Expression.Or(body, expressions[i]);
                }
            } else if (FixedValues?.Length > 0) {
                body = Expression.Equal(body, getFilter());
            } else { body = Expression.Equal(body, getExpressionConstant(FixedValue)); }
            var predicate = body;
            return Expression.Lambda<Func<TData, bool>>(predicate, param);
        }
        internal Expression<Func<TData, bool>> searchExpr() {
            var param = Expression.Parameter(typeof(TData), "s");
            Expression predicate = null;
            foreach (var p in typeof(TData).GetProperties()) {
                if ((p.PropertyType != typeof(string)) && p.PropertyType.IsClass) 
                    if (customExpr(p, param, predicate)) continue;
                predicate = whereExpr(param, p, predicate);
            }
            return predicate is null ? null : Expression.Lambda<Func<TData, bool>>(predicate, param);
        }
        protected Expression whereExpr(Expression param, PropertyInfo p, Expression predicate) {
            Expression body = Expression.Property(param, p);
            if (p.PropertyType == typeof(bool)) return predicate;
            if (p.PropertyType.IsEnum) return predicate;
            if (p.PropertyType != typeof(string))
                body = Expression.Call(body, "ToString", null);
            body = Expression.Call(body, "Contains", null, getExpressionConstant(SearchString));
            return predicate is null ? body : Expression.Or(predicate, body);
        }
        protected virtual bool customExpr(PropertyInfo p, ParameterExpression param, Expression predicate) => false;
        public List<TDomain> Get(string propertyName, string propertyValue) {
            FixedFilter = propertyName;
            FixedValue = propertyValue;
            return Get();
        }
    }
}