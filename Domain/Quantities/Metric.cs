using System.Collections.Generic;
using Abc.Data.Common;

namespace Abc.Domain.Quantities {

    public abstract class Metric<TData, TTerm> :BaseMetric<TData>
        where TData : MetricBaseData, new()
        where TTerm : ITerm {
        protected Metric(TData data = null) : base(data) { }
        public abstract IReadOnlyList<TTerm> Terms { get; }
    }
}