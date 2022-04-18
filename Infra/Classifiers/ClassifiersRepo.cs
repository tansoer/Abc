using System;
using System.Linq;
using System.Linq.Expressions;
using Abc.Aids.Extensions;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Infra.Common;

namespace Abc.Infra.Classifiers {
    public sealed class ClassifiersRepo : PagedRepo<IClassifier, ClassifierData>, IClassifiersRepo {
        public ClassifiersRepo(ClassifierDb c = null) : base(c, c?.Classifiers) { }
        protected internal override IClassifier toDomainObject(ClassifierData data) => ClassifierFactory.Create(data);
        private Expression getEnumExpressions() {
            var types = Enum.GetValues<ClassifierType>().ToList();
            var param = Expression.Parameter(typeof(ClassifierData), "x");
            Expression predicate = null;
            foreach (var t in types.Where(t => t.ToStr().Contains(FixedValue))) {
                Expression body = Expression.Equal(Expression.Property(param, "Type"), Expression.Constant(t));
                predicate = predicate is null ? body : Expression.Or(predicate, body);
            }
            return predicate is null ? null : Expression.Lambda<Func<ClassifierData, bool>>(predicate, param);
        }
        internal protected override IQueryable<ClassifierData> addCustomFilter(IQueryable<ClassifierData> q)
            => FixedValue != null
               && FixedFilter == nameof(ClassifierData.ClassifierType)
               && getEnumExpressions() != null
                ? q.Where((Expression<Func<ClassifierData, bool>>)getEnumExpressions())
                : base.addCustomFilter(q);
    }
}
