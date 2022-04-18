using Abc.Data.Quantities;
using Abc.Domain.Common;

namespace Abc.Domain.Quantities {
    public abstract class Unit : BaseMetric<UnitData> {

        protected UnitTerms unitTerms;
        protected UnitData unitData;
        protected DerivedUnit unit;
        protected CommonTermData termData;
        protected UnitTerm term;
        private readonly Measure measure;
        protected Unit() : this(null){}
        protected Unit(UnitData d) : base(d) {}
        protected Unit(UnitData d, Measure m) : this(d) => measure = m;
        public string MeasureId => Data?.MeasureId ?? Unspecified.String;
        public Measure Measure => measure ?? item<IMeasuresRepo, Measure>(MeasureId);
        public abstract double ToBase(in double amount);
        public abstract double FromBase(in double amount);
        public Unit Divide(Unit u, string n = null, string c = null, string d = null)
            => Multiply(u.Inverse(), n, c, d);
        public Unit Multiply(Unit u, string n = null, string c = null, string d = null) {
            if (u is DerivedUnit x) return toIdIsFormula(multiply(x, n, c, d));
            if (u is FunctionedUnit f) return toIdIsFormula(multiply(f, n, c, d));
            return toIdIsFormula(multiply(u as FactoredUnit, n, c, d));
        }
        protected abstract Unit multiply(DerivedUnit u, string n = null, string c = null, string d = null);
        protected abstract Unit multiply(FactoredUnit u, string n = null, string c = null, string d = null);
        protected abstract Unit multiply(FunctionedUnit u, string n = null, string c = null, string d = null);
        public Unit Inverse(string n = null, string c = null, string d = null) => Power(-1, n, c, d);
        public Unit Power(double power, string n = null, string c = null, string d = null)
            => toIdIsFormula(toPower(power, n, c, d));
        public Unit Sqrt(string n = null, string c = null, string d = null) => Power(0.5, n, c, d);
        private static Unit toIdIsFormula(Unit u) {
            u.data.Id = u.Formula();
            return u;
        }
        public string Formula(in bool isLong = false) {
            var f = formula(isLong);
            return string.IsNullOrWhiteSpace(f) ? Unspecified.String : f;
        }
        protected abstract string formula(bool isLong = false);
        protected void createUnit(Measure m, string n = null, string c = null, string d = null) {
            unitTerms = new UnitTerms();
            unitData = new UnitData(m.Id, n, c, d, UnitType.Derived);
            unit = new DerivedUnit(unitData, m, unitTerms);
        }
        protected void addTerm(Unit u, double power) {
            termData = new CommonTermData(unitData.Id, power, u.Id);
            term = unitTerms.Find(x => x.TermUnit.Name == u.Name);
            if (!(term is null)) {
                termData.Power = power + term.Power;
                unitTerms.Remove(term);
            }
            term = new UnitTerm(termData, unit, u);
            unitTerms.Add(term);
        }
        protected abstract Unit toPower(in double power, string n = null, string c = null, string d = null);
    }
}
