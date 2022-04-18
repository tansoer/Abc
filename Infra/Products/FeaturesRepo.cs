using Abc.Data.Common;
using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Infra.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace Abc.Infra.Products {

    public sealed class FeaturesRepo : EntityRepo<Feature, FeatureData>,
        IFeaturesRepo {
        public FeaturesRepo(ProductDb c = null) : base(c, c?.Features) { }
        protected override bool customExpr(PropertyInfo p, ParameterExpression param, Expression predicate) {
            if (p.PropertyType != typeof(ValueData)) return false;
            var newParam = Expression.Property(param, p);
            foreach (var x in typeof(ValueData).GetProperties()) 
                predicate = whereExpr(newParam, x, predicate);
            return true;
        }
    }
}