using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Infra.Common;

namespace Abc.Infra.Products {

    public sealed class FeatureTypesRepo : EntityRepo<FeatureType, FeatureTypeData>,
        IFeatureTypesRepo {
        public FeatureTypesRepo(ProductDb c = null) : base(c, c?.FeatureTypes) { }
    }
}