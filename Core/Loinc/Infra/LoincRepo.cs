using Abc.Core.Loinc.Data;
using Abc.Core.Loinc.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Abc.Core.Loinc.Infra {
    public sealed class LoincRepo : ILoincRepo {
        private LoincContext db;
        public LoincRepo(DbContext c) => db = c.GetService<LoincContext>();
        public async Task<List<LoincResponse>> GetAsync() => await db.LoincSet.ToListAsync();
        public List<LoincResponse> Get() => db.GetService<LoincContext>().LoincSet.ToList();
        public async Task<LoincResponse> GetAsync(string id) => await db.LoincSet.FirstOrDefaultAsync(x => x.Component.Contains(id));
        public async Task<List<LoincResponse>> GetTerms(string termName) => 
            await db.LoincSet.Where(x => x.Component.Contains(termName) && x.TimeAspect.Contains("Pt") && x.Scale.Contains("Qn") && x.System.Contains("Patient")).ToListAsync();
        public LoincResponse Get(string id) => db.LoincSet.FirstOrDefault(x => x.Component.Contains(id));
        public async Task<List<LoincResponse>> GetTermsByRule(string termName) 
            => await FilterByCompositeString($"Scale=Qn;TimeAspect=Pt;System=Patient;Component={termName}");
        public async Task<List<LoincResponse>> FilterByExpression(Expression expression) 
            => await db.LoincSet.Where((Expression<Func<LoincResponse, bool>>)expression).ToListAsync();
        public async Task<List<LoincResponse>> FilterByCompositeString(string composite) {
            var param = Expression.Parameter(typeof(LoincResponse), "x");
            var expressions = createExpressionsFromComposite(composite, param);
            Expression body = createAndExpression(expressions);
            var lambdaExpr = Expression.Lambda<Func<LoincResponse, bool>>(body, param);
            var list = await db.LoincSet.Where(lambdaExpr).ToListAsync();
            return list;
        }
        private List<string> separateCompositeString(string composite) => composite.Split(';').ToList();
        private List<Expression> createExpressionsFromComposite(string composite, Expression param) {
            var expressions = new List<Expression>();
            foreach (var condition in separateCompositeString(composite)) {
                expressions.Add(createContainsExpression(condition, param));
            }
            return expressions;
        }
        private Expression createContainsExpression(string condition, Expression param) {
            var property = condition.Remove(condition.IndexOf('='));
            var value = condition.Substring(condition.IndexOf('=') + 1);
            Expression body = Expression.Property(param, property);
            return Expression.Call(body, "Contains", null, Expression.Constant(value));
        }
        private Expression createAndExpression(List<Expression> expressions) {
            Expression body = expressions[0];
            for (int i = 1; i < expressions.Count; i++)
                body = Expression.And(body, expressions[i]);
            return body;
        }
    }
}
