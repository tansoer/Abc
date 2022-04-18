using Abc.Aids.Methods;
using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class FeatureTypeViewFactory :AbstractViewFactory<FeatureTypeData, FeatureType, FeatureTypeView> {
        protected internal override FeatureType toObject(FeatureTypeData d) => new(d);
    }

}