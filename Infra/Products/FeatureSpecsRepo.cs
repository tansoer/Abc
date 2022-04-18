using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Infra.Common;

namespace Abc.Infra.Products {
    public sealed class FeatureSpecsRepo :
        EntityRepo<FeatureSpec, FeatureSpecData>,
        IFeatureSpecsRepo {
        public FeatureSpecsRepo(ProductDb c = null) : base(c, c?.FeatureSpecs) { }
    }
}