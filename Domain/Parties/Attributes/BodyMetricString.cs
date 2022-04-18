using Abc.Data.Parties;

namespace Abc.Domain.Parties.Attributes {

    public sealed class BodyMetricString : BodyMetric<string> {

        public BodyMetricString() : this(null) { }
        public BodyMetricString(BodyMetricData d) : base(d) { }
        public override string Value => get(Data?.Value);

        public override string ToString() => Value;
    }
}