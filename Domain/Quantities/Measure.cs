using System.Collections.Generic;
using Abc.Aids.Reflection;
using Abc.Data.Quantities;
using Abc.Domain.Common;

namespace Abc.Domain.Quantities {

    public abstract class Measure : BaseMetric<MeasureData> {
        protected MeasureTerms measureTerms;
        protected MeasureData measureData;
        protected DerivedMeasure measure;
        protected CommonTermData termData;
        protected MeasureTerm term;
        private static string measureId => GetMember.Name<UnitData>(x => x.MeasureId);
        protected Measure() : this(null) { }
        protected Measure(MeasureData d) : base(d) { }
        public IReadOnlyList<Unit> Units => list<IUnitsRepo, Unit>(measureId, Id);

        public Measure Multiply(Measure m, string name = null, string c = null, string d = null) {
            if (m is DerivedMeasure x) return toIdIsFormula(multiply(x, name, c, d));
            return toIdIsFormula(multiply(m as BaseMeasure, name, c, d));
        }
        public Measure Power(double i, string n = null, string c = null, string d = null)
            => toIdIsFormula(power(i, n, c, d));
        public Measure Sqrt(string n = null, string c = null, string d = null) => Power(0.5, n, c, d);
        public Measure Divide(Measure m, string n = null, string c = null, string d = null) 
            => (m is DerivedMeasure x)? divide(x, n, c, d): divide(m as BaseMeasure, n, c, d);
        protected Measure divide(DerivedMeasure m, string n = null, string c = null, string d = null)
            => Multiply(m.Inverse(), n, c, d);
        protected Measure divide(BaseMeasure m, string name = null, string c = null, string d = null)
            => Multiply(m.Inverse(), name, c, d);
        protected abstract Measure multiply(BaseMeasure m, string n = null, string c = null, string d = null);
        protected abstract Measure multiply(DerivedMeasure m, string n = null, string c = null, string d = null);
        public Measure Inverse(string n = null, string c = null, string d = null) => Power(-1, n, c, d);
        private static Measure toIdIsFormula(Measure m) {
            m.data.Id = m.Formula();
            return m;
        }
        protected abstract Measure power(in double i, string n = null, string c = null, string d = null);
        public string Formula(in bool isLong = false) {
            var f = formula(isLong);
            return string.IsNullOrWhiteSpace(f) ? Unspecified.String : f;
        }
        protected abstract string formula(bool isLong = false);
        protected void createMeasure(string n = null, string c = null, string d = null) {
            measureTerms = new MeasureTerms();
            measureData = new MeasureData(n, c, d, MeasureType.Derived);
            measure = new DerivedMeasure(measureData, measureTerms);
        }
        protected void addTerm(Measure m, double power) {
            termData = new CommonTermData(measureData.Id, power, m.Id);
            term = measureTerms.Find(x => x.TermMeasure.Name == m.Name);
            if (term is not null) {
                termData.Power = power + term.Power;
                measureTerms.Remove(term);
            }
            term = new MeasureTerm(termData, measure, m);
            measureTerms.Add(term);
        }
    }
}
