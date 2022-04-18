using Abc.Data.Parties;

namespace Abc.Domain.Roles {
    public sealed class PartySummary :BasePartySummary {
        public PartySummary() : this(null) { }
        public PartySummary(PartySummaryData d) : base(d) { }
    }
}


