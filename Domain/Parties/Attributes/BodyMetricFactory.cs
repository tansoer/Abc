using Abc.Aids.Enums;
using Abc.Data.Parties;

namespace Abc.Domain.Parties.Attributes
{
    public static class BodyMetricFactory {
        public static IBodyMetric Create(BodyMetricData d) =>
            d?.MetricType switch {
                MetricType.String => new BodyMetricString(d),
                MetricType.Quantity => new BodyMetricQuantity(d),
                _ => new BodyMetricString(d)
            };

        public static BodyMetricData Create(IBodyMetric obj) => obj.Data;
    }
}