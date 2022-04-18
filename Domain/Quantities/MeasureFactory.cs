using Abc.Data.Quantities;

namespace Abc.Domain.Quantities {

    public static class MeasureFactory {
        public static Measure Create(MeasureData d = null) => d?.MeasureType switch {
                MeasureType.Derived => new DerivedMeasure(d),
                _ => new BaseMeasure(d)
            };
    }
}
