using System.ComponentModel;
using Abc.Aids.Enums;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {
    public sealed class BodyMetricView : PartyAttributeView {

        [DisplayName("Value")] public double QuantityValue { get; set; }
        [DisplayName("Value")] public string StringValue { get; set; }
        [DisplayName("Value")] public string Value => ToString();
        [DisplayName("Signed by")] public string SignatureId { get; set; }
        [DisplayName("Type")] public string TypeId { get; set; }
        [DisplayName("Unit")] public string UnitId { get; set; }
        [DisplayName("Metric type")] public MetricType MetricType { get; set; }
        public override string ToString() => MetricType == MetricType.Quantity ? $"{QuantityValue} {UnitId}" : StringValue;
    }
}
