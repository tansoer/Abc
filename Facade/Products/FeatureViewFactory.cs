using Abc.Aids.Methods;
using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Domain.Values;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class FeatureViewFactory :AbstractViewFactory<FeatureData, Feature, FeatureView>{
        protected internal override Feature toObject(FeatureData d) => new(d);
    }
}