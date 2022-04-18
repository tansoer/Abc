
using Abc.Aids.Enums;

namespace Abc.Data.Parties {
    public sealed class BodyMetricData : PartyAttributeData {
        public string SignatureId { get; set; }
        public string TypeId { get; set; }
        public string Value { get; set; }
        public string UnitId { get; set; }
        public MetricType MetricType { get; set; }
    }
}