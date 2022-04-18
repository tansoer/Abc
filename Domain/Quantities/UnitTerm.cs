using Abc.Data.Quantities;
using Abc.Domain.Common;

namespace Abc.Domain.Quantities {

    public sealed class UnitTerm : BaseTerm {
        private readonly DerivedUnit master;
        private readonly Unit term;
        public UnitTerm() : this(null) { }
        public UnitTerm(CommonTermData d) : base(d) { }
        public UnitTerm(CommonTermData d, DerivedUnit m, Unit t) : this(d) {
            master = m;
            term = t;
        }
        public string MasterUnitId => Data?.MasterId ?? Unspecified.String;
        public string TermUnitId => Data?.TermId ?? Unspecified.String;
        public DerivedUnit MasterUnit => master ?? new GetFrom<IUnitsRepo, Unit>().ById(MasterUnitId) as DerivedUnit;
        public Unit TermUnit => term ?? new GetFrom<IUnitsRepo, Unit>().ById(TermUnitId);
        public string Formula(in bool isLong = false) => isLong ? formula(TermUnit.Name) : formula(TermUnit.Code);
    }
}
