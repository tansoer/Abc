using Abc.Data.Common;
using Abc.Domain.Common;

namespace Abc.Domain.Quantities {

    public abstract class BaseMetric<TData> :Entity<TData>
        where TData : MetricBaseData, new() {
        public BaseMetric() : this(null) { }
        public BaseMetric(TData d) : base(d) { }
        public override string Code => string.IsNullOrWhiteSpace(Data?.Code) ? Name : Data?.Code;
        public override string Name => string.IsNullOrWhiteSpace(Data?.Name) ? Id : Data?.Name;
        public override string Id => IsUnspecified || string.IsNullOrWhiteSpace(Data?.Id) ? Unspecified.String : Data?.Id;
    }
}