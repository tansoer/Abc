using Abc.Aids.Methods;
using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Domain.Values;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class FeatureSpecViewFactory :AbstractViewFactory<FeatureSpecData, FeatureSpec, FeatureSpecView> {
        protected internal override FeatureSpec toObject(FeatureSpecData d) => new(d);
        public override FeatureSpecView Create(FeatureSpec o) {
            var v = base.Create(o);
            FeatureSpecViewValueFactory.Create(v, o.Value);
            return v;
        }
        public override FeatureSpec Create(FeatureSpecView v) {
            var d = new FeatureSpecData();
            Copy.Members(v, d);
            d.Value = (FeatureSpecViewValueFactory.Create(v) as BaseCommonValue)?.Data;
            return new FeatureSpec(d);
        }
    }
}