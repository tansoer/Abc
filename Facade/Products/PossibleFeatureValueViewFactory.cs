using Abc.Aids.Methods;
using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Domain.Values;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class PossibleFeatureValueViewFactory :AbstractViewFactory<PossibleFeatureValueData, PossibleFeatureValue, PossibleFeatureValueView>{

        //TODO Gunnar: Urgent
        //public override PossibleFeatureValueView Create(PossibleFeatureValue o) {
        //    var v = base.Create(o);
        //    FeatureValueViewFactory.Create(v, o.Value);
        //    return v;
        //}
        //public override PossibleFeatureValue Create(PossibleFeatureValueView v) {
        //    var d = new PossibleFeatureValueData();
        //    Copy.Members(v, d);
        //    d.Value = (FeatureValueViewFactory.Create(v) as BaseCommonValue)?.Data;
        //    return new PossibleFeatureValue(d);
        //}

        protected internal override PossibleFeatureValue toObject(PossibleFeatureValueData d) => new (d);
    }
}