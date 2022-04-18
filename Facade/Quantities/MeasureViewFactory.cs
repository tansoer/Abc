using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Common;

namespace Abc.Facade.Quantities {

    public sealed class MeasureViewFactory : AbstractViewFactory<MeasureData, Measure, MeasureView> {

        public override MeasureView Create(Measure o) {
            var v = base.Create(o);
            v.MeasureType = o switch
            {
                BaseMeasure => MeasureType.Base,
                DerivedMeasure => MeasureType.Derived,
                _ => MeasureType.Unspecified
            };
            v.Formula = o.Formula(true);
            return v;
        }

        protected internal override Measure toObject(MeasureData d) => MeasureFactory.Create(d);
    }

}
