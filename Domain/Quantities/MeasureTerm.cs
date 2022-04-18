using Abc.Data.Quantities;
using Abc.Domain.Common;

namespace Abc.Domain.Quantities {
    public sealed class MeasureTerm : BaseTerm {
        private readonly DerivedMeasure master;
        private readonly Measure term;
        public MeasureTerm() : this(null) { }
        public MeasureTerm(CommonTermData d) : base(d) { }
        public MeasureTerm(CommonTermData d, DerivedMeasure m, Measure t) : this(d) {
            master = m;
            term = t;
        }
        public string MasterMeasureId => Data?.MasterId ?? Unspecified.String;
        public string TermMeasureId => Data?.TermId ?? Unspecified.String;
        public DerivedMeasure MasterMeasure => master ?? new GetFrom<IMeasuresRepo, Measure>().ById(MasterMeasureId) as DerivedMeasure;
        public Measure TermMeasure => term ?? new GetFrom<IMeasuresRepo, Measure>().ById(TermMeasureId);
        public string Formula(in bool isLong = false) => isLong ? formula(TermMeasure.Name) : formula(TermMeasure.Code);
    }
}
