using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Reflection;
using Abc.Data.Quantities;
using Abc.Domain.Common;

namespace Abc.Domain.Quantities {

    public sealed class DerivedMeasure : Measure {
        private readonly MeasureTerms terms;
        internal static string field => GetMember.Name<CommonTermData>(x => x.MasterId);
        public DerivedMeasure() : this(null) { }
        public DerivedMeasure(MeasureData data) : base(data) { }
        public DerivedMeasure(MeasureData d, MeasureTerms l) : this(d) => terms = l;
        public IReadOnlyList<MeasureTerm> Terms => terms?.AsReadOnly() ?? new GetFrom<IMeasureTermsRepo, MeasureTerm>().ListBy(field, Id);
        protected override Measure multiply(BaseMeasure m, string n = null, string c = null, string d = null) {
            createMeasure(n, c, d);
            foreach (var t in Terms) addTerm(t.TermMeasure, t.Power);
            addTerm(m, 1);
            return measure;
        }
        protected override Measure multiply(DerivedMeasure m, string n = null, string c = null, string d = null) {
            createMeasure(n, c, d);
            foreach (var t in Terms) addTerm(t.TermMeasure, t.Power);
            foreach (var t in m.Terms) addTerm(t.TermMeasure, t.Power);
            return measure;
        }
        protected override Measure power(in double i, string n = null, string c = null, string d = null) {
            createMeasure(n, c, d);
            foreach (var t in Terms) addTerm(t.TermMeasure, t.Power * i);
            return measure;
        }
        protected override string formula(bool isLong = false) {
            var d = Terms.ToDictionary(e => e.Formula(isLong), e => e.Power);
            d = d.OrderByDescending(x => x.Value, new comparer()).ToDictionary(pair => pair.Key, pair => pair.Value);
            var l = d.Where(e => e.Value != 0).Select(e => e.Key).ToList();
            return l.Aggregate(string.Empty, (c, s) => c == string.Empty ? s : $"{c} * {s}");
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