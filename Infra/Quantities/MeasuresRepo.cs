using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;

namespace Abc.Infra.Quantities {

    public sealed class MeasuresRepo : PagedRepo<Measure, MeasureData>, IMeasuresRepo {
        public MeasuresRepo(QuantityDb c = null) : base(c, c?.Measures) { }
        protected internal override Measure toDomainObject(MeasureData d) => MeasureFactory.Create(d);
    }
}
