using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Reflection;
using Abc.Data.Quantities;

namespace Abc.Domain.Quantities {
    public sealed class DerivedUnit : Unit {
        private readonly UnitTerms terms;
        internal static string field => GetMember.Name<CommonTermData>(x => x.MasterId);
        public DerivedUnit() : this(null) { }
        public DerivedUnit(UnitData d) : base(d) { }
        public DerivedUnit(UnitData d, Measure m, UnitTerms t = null) : base(d, m) => terms = t;
        public IReadOnlyList<UnitTerm> Terms => terms?.AsReadOnly() ?? list<IUnitTermsRepo, UnitTerm>(field, Id);
        public override double ToBase(in double d) {
            var r = d;
            foreach (var t in Terms) {
                var u = t.TermUnit;
                var p = t.Power;
                if (p == 0) continue;
                if (p > 0) for (var i = 1; i <= p; i++) r *= u.ToBase(1);
                else for (var i = -1; i >= p; i--) r /= u.ToBase(1);
            }
            return r;
        }
        public override double FromBase(in double d) {
            var r = d;
            foreach (var t in Terms) {
                var u = t.TermUnit;
                var p = t.Power;
                if (p == 0) continue;
                if (p > 0) for (var i = 1; i <= p; i++) r *= u.FromBase(1);
                else for (var i = -1; i >= p; i--) r /= u.FromBase(1);
            }
            return r;
        }
        protected override Unit multiply(DerivedUnit u, string n = null, string c = null, string d = null) {
            var m = Measure.Multiply(u.Measure);
            createUnit(m, n, c, d);
            foreach (var t in Terms) addTerm(t.TermUnit, t.Power);
            foreach (var t in u.Terms) addTerm(t.TermUnit, t.Power);
            return unit;
        }
        protected override Unit multiply(FactoredUnit u, string n = null, string c = null, string d = null) {
            var m = Measure.Multiply(u.Measure);
            createUnit(m, n, c, d);
            foreach (var t in Terms) addTerm(t.TermUnit, t.Power);
            addTerm(u, 1);
            return unit;
        }
        protected override Unit multiply(FunctionedUnit u, string n = null, string c = null, string d = null) {
            var m = Measure.Multiply(u.Measure);
            createUnit(m, n, c, d);
            foreach (var t in Terms) addTerm(t.TermUnit, t.Power);
            addTerm(u, 1);
            return unit;
        }
        protected override Unit toPower(in double power, string n = null, string c = null, string d = null) {
            var m = Measure.Power(power);
            createUnit(m, n, c, d);
            foreach (var t in Terms) addTerm(t.TermUnit, t.Power * power);
            return unit;
        }
        protected override string formula(bool isLong = false) {
            var d = Terms.ToDictionary(e => e.Formula(isLong), e => e.Power);
            d = d.OrderByDescending(x => x.Value, new comparer()).ToDictionary(pair => pair.Key, pair => pair.Value);
            var l = d.Where(e => e.Value != 0).Select(e => e.Key).ToList();
            return l.Aggregate(string.Empty, (c, s)
                => c == string.Empty ? s : $"{c} * {s}");
        }
        private class comparer : IComparer<double> {
            public int Compare(double x, double y) {
                if (x == y) return 0;
                if ((x > 0) & (y < 0)) return 1;
                if ((x < 0) & (y > 0)) return -1;
                x = Math.Abs(x);
                y = Math.Abs(y);
                if (x > y) return -1;
                if (x < y) return 1;
                return 0;
            }
        }
    }
}