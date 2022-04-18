using System.Collections.Generic;
using System.Linq;
using Abc.Data.Quantities;

namespace Abc.Domain.Quantities {

    public sealed class FactoredUnit : Unit {
        internal static string field => nameof(UnitFactorData.UnitId);
        public FactoredUnit(UnitData d = null) : base(d) { }
        public IReadOnlyList<UnitFactor> Factors => list<IUnitFactorsRepo, UnitFactor>(field, Id);
        public UnitFactor SiSystemFactor
            => Factors.FirstOrDefault(x => x.SystemOfUnitsId == Core.Units.SystemOfUnits.SiSystemId) ??
               Factors.FirstOrDefault() ??
               new UnitFactor();
        public double Factor => SiSystemFactor.Factor;
        public override double ToBase(in double amount) => amount * Factor;
        public override double FromBase(in double amount) => amount / Factor;
        protected override Unit multiply(DerivedUnit u, string n = null, string c = null, string d = null) {
            var m = Measure.Multiply(u.Measure);
            createUnit(m, n, c, d);
            addTerm(this, 1);
            foreach (var t in u.Terms) addTerm(t.TermUnit, t.Power);
            return unit;
        }
        protected override Unit multiply(FactoredUnit u, string n = null, string c = null, string d = null) {
            var m = Measure.Multiply(u.Measure);
            createUnit(m, n, c, d);
            addTerm(this, 1);
            addTerm(u, 1);
            return unit;
        }
        protected override Unit multiply(FunctionedUnit u, string n = null, string c = null, string d = null) {
            var m = Measure.Multiply(u.Measure);
            createUnit(m, n, c, d);
            addTerm(this, 1);
            addTerm(u, 1);
            return unit;
        }
        protected override Unit toPower(in double power, string n = null, string c = null, string d = null) {
            var m = Measure.Power(power);
            createUnit(m, n, c, d);
            addTerm(this, power);
            return unit;
        }
        protected override string formula(bool isLong = false) => $"{1/Factor}";
    }
}