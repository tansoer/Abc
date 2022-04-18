using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Infra.Common;

namespace Abc.Infra.Products {
    public sealed class PossibleFeatureValuesRepo :
        EntityRepo<PossibleFeatureValue, PossibleFeatureValueData>,
        IPossibleFeatureValuesRepo {
        public PossibleFeatureValuesRepo(ProductDb c = null) : base(c, c?.PossibleFeatureValues) { }
    }
}