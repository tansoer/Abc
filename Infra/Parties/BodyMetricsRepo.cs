using Abc.Aids.Enums;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class BodyMetricsRepo : PagedRepo<IBodyMetric, BodyMetricData>,
        IBodyMetricsRepo {
        public BodyMetricsRepo(PartyDb c = null) : base(c, c?.BodyMetrics) { }
        protected internal override IBodyMetric toDomainObject(BodyMetricData d) {
            if (d.MetricType == MetricType.Quantity) return new BodyMetricQuantity(d);
            return new BodyMetricString(d);
        }
    }
}

