using System;
using Abc.Aids.Extensions;
using System.Linq.Expressions;

namespace Abc.Aids.Values {
    public static class Compute {
        public static object Add(object x, object y) {
            if (TypesAre.Bool(x, y)) return ((bool)x).Add((bool)y);
            if (TypesAre.String(x, y)) return Strings.Add((string)x, (string)y);
            if (TypesAre.DateTime(x, y)) return ((DateTime)x).AddSafe((DateTime)y);
            if (TypesAre.AnyDecimal(x, y)) return ConvertTo.Decimal(x).Add(ConvertTo.Decimal(y));
            if (TypesAre.AnyDouble(x, y)) return Doubles.ToDouble(x).Add(Doubles.ToDouble(y));
            return null;
        }
        public static object Subtract(object x, object y) {
            if (TypesAre.DateTime(x, y)) return ((DateTime)x).SubtractSafe((DateTime)y);
            if (TypesAre.AnyDecimal(x, y)) return ConvertTo.Decimal(x).Subtract(ConvertTo.Decimal(y));
            if (TypesAre.AnyDouble(x, y)) return Doubles.ToDouble(x).Subtract(Doubles.ToDouble(y));
            return null;
        }
        public static object Multiply(object x, object y) {
            if (TypesAre.Bool(x, y)) return ((bool)x).Multiply((bool)y);
            if (TypesAre.AnyDecimal(x, y)) return ConvertTo.Decimal(x).Multiply(ConvertTo.Decimal(y));
            if (TypesAre.AnyDouble(x, y)) return Doubles.ToDouble(x).Multiply(Doubles.ToDouble(y));
            return null;
        }
        public static object Divide(object x, object y) {
            if (TypesAre.AnyDecimal(x, y)) return ConvertTo.Decimal(x).Divide(ConvertTo.Decimal(y));
            if (TypesAre.AnyDouble(x, y)) return Doubles.ToDouble(x).Divide(Doubles.ToDouble(y));
            return null;
        }
        public static object Power(object x, object y) {
            if (TypesAre.AnyDouble(x, y)) return Doubles.ToDouble(x).Power(Doubles.ToDouble(y));
            return null;
        }
        public static object Inverse(object x) {
            if (TypesAre.AnyDecimal(x)) return ConvertTo.Decimal(x).Inverse();
            if (TypesAre.AnyDouble(x)) return Doubles.ToDouble(x).Inverse();
            return null;
        }
        public static object Opposite(object x) {
            if (TypesAre.AnyDecimal(x)) return ConvertTo.Decimal(x).Opposite();
            if (TypesAre.AnyDouble(x)) return Doubles.ToDouble(x).Opposite();
            return null;
        }
        public static object Square(object x) {
            if (TypesAre.AnyDouble(x)) return Doubles.ToDouble(x).Square();
            return null;
        }
        public static object Sqrt(object x) {
            if (TypesAre.AnyDouble(x)) return Doubles.ToDouble(x).Sqrt();
            return null;
        }
        public static object And(object x, object y) {
            if (TypesAre.Bool(x, y)) return ((bool)x).And((bool)y);
            return null;
        }
        public static object Or(object x, object y) {
            if (TypesAre.Bool(x, y)) return ((bool)x).Or((bool)y);
            return null;
        }
        public static object Xor(object x, object y) {
            if (TypesAre.Bool(x, y)) return ((bool)x).Xor((bool)y);
            return null;
        }
        public static object Not(object x) {
            if (TypesAre.Bool(x)) return ((bool)x).Not();
            return null;
        }
        public static object IsEqual(object x, object y) {
            if (TypesAre.Bool(x, y)) return ((bool)x).IsEqual((bool)y);
            if (TypesAre.String(x, y)) return ((string)x).IsEqual((string)y);
            if (TypesAre.DateTime(x, y)) return ((DateTime)x).IsEqual((DateTime)y);
            if (TypesAre.AnyDecimal(x, y)) return ConvertTo.Decimal(x).IsEqual(ConvertTo.Decimal(y));
            if (TypesAre.AnyDouble(x, y)) return Doubles.ToDouble(x).IsEqual(Doubles.ToDouble(y));
            return null;
        }
        public static object IsGreater(object x, object y) {
            if (TypesAre.Bool(x, y)) return ((bool)x).IsGreater((bool)y);
            if (TypesAre.String(x, y)) return ((string)x).IsGreater((string)y);
            if (TypesAre.DateTime(x, y)) return ((DateTime)x).IsGreater((DateTime)y);
            if (TypesAre.AnyDecimal(x, y)) return ConvertTo.Decimal(x).IsGreater(ConvertTo.Decimal(y));
            if (TypesAre.AnyDouble(x, y)) return Doubles.ToDouble(x).IsGreater(Doubles.ToDouble(y));
            return null;
        }
        public static object IsLess(object x, object y) {
            if (TypesAre.Bool(x, y)) return ((bool)x).IsLess((bool)y);
            if (TypesAre.String(x, y)) return ((string)x).IsLess((string)y);
            if (TypesAre.DateTime(x, y)) return ((DateTime)x).IsLess((DateTime)y);
            if (TypesAre.AnyDecimal(x, y)) return ConvertTo.Decimal(x).IsLess(ConvertTo.Decimal(y));
            if (TypesAre.AnyDouble(x, y)) return Doubles.ToDouble(x).IsLess(Doubles.ToDouble(y));
            return null;
        }
        public static object GetSecond(object x) {
            if (TypeIs.DateTime(x)) return ((DateTime)x).GetSecond();
            if (TypeIs.TimeSpan(x)) return ((TimeSpan)x).Seconds;
            return null;
        }
        public static object GetMinute(object x) {
            if (TypeIs.DateTime(x)) return ((DateTime)x).GetMinute();
            if (TypeIs.TimeSpan(x)) return ((TimeSpan)x).Minutes;
            return null;
        }
        public static object GetHour(object x) {
            if (TypeIs.DateTime(x)) return ((DateTime)x).GetHour();
            if (TypeIs.TimeSpan(x)) return ((TimeSpan)x).Hours;
            return null;
        }
        public static object GetDay(object x) {
            if (TypeIs.DateTime(x)) return ((DateTime)x).GetDay();
            if (TypeIs.TimeSpan(x)) return ((TimeSpan)x).Days;
            return null;
        }
        public static object GetMonth(object x) {
            if (TypeIs.DateTime(x)) return ((DateTime)x).GetMonth();
            return null;
        }
        public static object GetYear(object x) {
            if (TypeIs.DateTime(x)) return ((DateTime)x).GetYear();
            return null;
        }
        public static object GetInterval(object x, object y) {
            if (TypesAre.DateTime(x, y)) return ((DateTime)x).GetInterval((DateTime)y).Ticks;
            return null;
        }
        public static object GetAge(object x) {
            if (TypeIs.DateTime(x)) return ((DateTime)x).GetAge();
            return null;
        }
        public static object ToSeconds(object x) {
            if (TypeIs.TimeSpan(x)) return ((TimeSpan)x).Seconds();
            if (TypeIs.DateTime(x)) return new TimeSpan(((DateTime)x).Ticks).Seconds();
            return null;
        }
        public static object ToMinutes(object x) {
            if (TypeIs.TimeSpan(x)) return ((TimeSpan)x).Minutes();
            if (TypeIs.DateTime(x)) return new TimeSpan(((DateTime)x).Ticks).Minutes();
            return null;
        }
        public static object ToHours(object x) {
            if (TypeIs.TimeSpan(x)) return ((TimeSpan)x).Hours();
            if (TypeIs.DateTime(x)) return new TimeSpan(((DateTime)x).Ticks).Hours();
            return null;
        }
        public static object ToDays(object x) {
            if (TypeIs.TimeSpan(x)) return ((TimeSpan)x).Days();
            if (TypeIs.DateTime(x)) return new TimeSpan(((DateTime)x).Ticks).Days();
            return null;
        }
        public static object ToMonths(object x) {
            if (TypeIs.TimeSpan(x)) return ((TimeSpan)x).Months();
            if (TypeIs.DateTime(x)) return new TimeSpan(((DateTime)x).Ticks).Months();
            return null;
        }
        public static object ToYears(object x) {
            if (TypeIs.TimeSpan(x)) return ((TimeSpan)x).Years();
            if (TypeIs.DateTime(x)) return new TimeSpan(((DateTime)x).Ticks).Years();
            return null;
        }
        public static object AddSeconds(object x, object y) {
            if (!TypeIs.AnyDouble(y)) return null;
            if (TypeIs.DateTime(x)) return ((DateTime)x).AddSecondsSafe(Doubles.ToDouble(y));
            return null;
        }
        public static object AddMinutes(object x, object y) {
            if (!TypeIs.AnyDouble(y)) return null;
            if (TypeIs.DateTime(x)) return ((DateTime)x).AddMinutesSafe(Doubles.ToDouble(y));
            return null;
        }
        public static object AddHours(object x, object y) {
            if (!TypeIs.AnyDouble(y)) return null;
            if (TypeIs.DateTime(x)) return ((DateTime)x).AddHoursSafe(Doubles.ToDouble(y));
            return null;
        }
        public static object AddDays(object x, object y) {
            if (!TypeIs.AnyDouble(y)) return null;
            if (TypeIs.DateTime(x)) return ((DateTime)x).AddDaysSafe(Doubles.ToDouble(y));
            return null;
        }
        public static object AddMonths(object x, object y) {
            if (!TypeIs.AnyInt(y)) return null;
            if (TypeIs.DateTime(x)) return ((DateTime)x).AddMonthsSafe(Integers.ToInteger(y));
            return null;
        }
        public static object AddYears(object x, object y) {
            if (!TypeIs.AnyInt(y)) return null;
            if (TypeIs.DateTime(x)) return ((DateTime)x).AddYearsSafe(Integers.ToInteger(y));
            return null;
        }
        public static object GetLength(object x) {
            if (TypeIs.String(x)) return Strings.GetLength((string)x);
            return null;
        }
        public static object ToUpper(object x) {
            if (TypeIs.String(x)) return Strings.ToUpper((string)x);
            return null;
        }
        public static object ToLower(object x) {
            if (TypeIs.String(x)) return Strings.ToLower((string)x);
            return null;
        }
        public static object Trim(object x) {
            if (TypeIs.String(x)) return Strings.Trim((string)x);
            return null;
        }
        internal static object substring(object x, object y) {
            if (TypeIs.String(x) && TypeIs.AnyInt(y)) return Strings.Substring((string)x, Integers.ToInteger(y));
            return null;
        }
        internal static object substring(object x, object y, object z) {
            if (!TypeIs.String(x) || !TypesAre.AnyInt(y, z)) return null;
            var r = Strings.Substring((string)x, Integers.ToInteger(y), Integers.ToInteger(z));
            return r;
        }
        public static object Substring(object x, object y, object z = null) 
            => (z is null)? substring(x, y): substring(x, y, z);
        public static object Contains(object x, object y) 
            => TypesAre.String(x, y)? Strings.Contains((string)x, (string)y): null;
        public static object EndsWith(object x, object y) 
            => TypesAre.String(x, y)?Strings.EndsWith((string)x, (string)y): null;
        public static object StartsWith(object x, object y) {
            if (TypesAre.String(x, y)) return Strings.StartsWith((string)x, (string)y);
            return null;
        }
        public static object Dummy() => null;
        public static object Exception(Exception e = null) => e;
        public static object ToExpression(object x, object y, string operation, Expression param) {
            var v = getValue<string>(x);
            Expression body = Expression.Property(param, v);
            return Expression.Call(body, operation, null, Expression.Constant(getValue<string>(y)));
        }
        private static TType getValue<TType>(object x)
            => (TType)x?.GetType().GetProperty("Value")?.GetValue(x, null);
    }
}