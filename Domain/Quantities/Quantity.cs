using System;
using Abc.Aids.Constants;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using Abc.Core.Rounding;
using Abc.Data.Quantities;
using Abc.Domain.Common;

namespace Abc.Domain.Quantities {

    public sealed class Quantity :MeasurableValue<Quantity, double, Unit> {
        public Quantity() : this(double.NaN, Word.Unspecified) { }
        public Quantity(double amount, Unit u) : base(amount, u ?? UnitFactory.Create()) { }
        public Quantity(double amount, string unitId) : base(amount, new GetFrom<IUnitsRepo, Unit>().ById(unitId)) { }
        public Unit Unit {
            get {
                var d = new UnitData { Id = Word.Unspecified };
                if (measure is not null) Copy.Members(measure.Data, d);
                return UnitFactory.Create(d);
            }
        }
        public static bool TryParse(string s, out Quantity q) {
            q = new Quantity();
            var amountStr = s.GetHead(' ');
            var unitStr = s.GetTail(' ');
            var r = double.TryParse(amountStr, out var amount);
            if (r) q = new Quantity(amount, unitStr);
            return r;
        }
        public static Quantity Parse(string s) => TryParse(s, out var q) ? q : new Quantity();
        public override Quantity Round(IRoundingPolicy roundingPolicy) {
            var a = roundingPolicy.Round(Amount);
            return new Quantity(a, Unit);
        }
        public override Quantity Add(Quantity q) {
            if (q is null) return Unspecified;
            if (!IsSameMeasure(q.Unit)) return Unspecified;
            var d = Unit.ToBase(Amount);
            d += q.Unit.ToBase(q.Amount);
            return new Quantity(q.Unit.FromBase(d), q.Unit);
        }
        public override int CompareTo(Quantity q) {
            if (q is null) return int.MinValue;
            var u2 = q.Unit;
            var u1 = Unit;
            var q2 = u2.ToBase(q.Amount);
            var q1 = u1.ToBase(Amount);
            return !IsSameMeasure(u2) ? int.MaxValue : CompareAlmostEqual(q1, q2);
        }
        public static int CompareAlmostEqual(double q1, double q2) {
            var d = Math.Abs(q1 - q2) / Math.Abs(Math.Max(q1, q2));
            if (d < 0.000000000000001) return 0;
            return q1.CompareTo(q2);
        }
        public override bool IsEqual(Quantity m) => CompareTo(m) == 0;
        public override bool IsLess(Quantity m) => CompareTo(m) == -1;
        public override bool IsGreater(Quantity m) => CompareTo(m) == 1;
        public override Quantity ConvertTo(Unit u) {
            if (u is null) return Unspecified;
            var d = convertTo(Amount, u);
            u = (double.IsNaN(d)) ? UnitFactory.Create() : u;
            return new Quantity(d, u);
        }
        public override Quantity Divide(double d) => new(Amount / d, Unit);
        public Quantity Divide(Quantity q) {
            if (q is null) return Unspecified;
            var d = Unit.ToBase(Amount);
            d /= q.Unit.ToBase(q.Amount);
            var u = Unit.Divide(q.Unit);
            return new Quantity(u.FromBase(d), u);
        }
        public static Quantity Unspecified => new(double.NaN, UnitFactory.Create());
        public override Quantity Multiply(double d) => new(Amount * d, Unit);
        public Quantity Multiply(Quantity q) {
            if (q is null) return Unspecified;
            var d = Unit.ToBase(Amount);
            d *= q.Unit.ToBase(q.Amount);
            var u = Unit.Multiply(q.Unit);
            var r = new Quantity(u.FromBase(d), u);
            return r;
        }
        public Quantity Inverse() => Power(-1);
        public Quantity Power(double power) {
            var u = Unit.Power(power);
            var a = Math.Pow(Amount, power);
            return new Quantity(a, u);
        }
        public override Quantity Subtract(Quantity q) {
            if (q is null) return Unspecified;
            if (!IsSameMeasure(q.Unit)) return Unspecified;
            var d = Unit.ToBase(Amount);
            d -= q.Unit.ToBase(q.Amount);
            return new Quantity(q.Unit.FromBase(d), q.Unit);
        }
        internal double convertTo(double d, Unit u) {
            if (u is null) return double.NaN;
            if (!IsSameMeasure(u)) return double.NaN;
            d = Unit.ToBase(d);
            return u.FromBase(d);
        }
        public bool IsSameMeasure(Unit u) => Unit.MeasureId == u?.MeasureId;
        public double ToBase() => Unit.ToBase(Amount);
        public Quantity Opposite() => new(-Amount, Unit);
        public Quantity Sqrt() => Power(0.5);
    }
}
