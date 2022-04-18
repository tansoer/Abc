using Abc.Aids.Enums;
using Abc.Data.Common;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Attributes;

namespace Abc.Domain.Parties.Signatures {
    public sealed class SignedEntityFactory {
        public static IEntity Create<TData>(TData d)
            where TData : EntityBaseData, new() {
            return d switch
            {
                BodyMetricData x => x.MetricType == MetricType.Quantity ? (IBodyMetric) new BodyMetricQuantity(x) : new BodyMetricString(x),
                _ => null
            };
        }
    }
}