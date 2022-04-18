using System;
using System.ComponentModel;
using Abc.Aids.Methods;
using System.Globalization;

namespace Abc.Aids.Extensions {
    public static class Variable {
        public static string ToString<T>(T v)
            => Safe.Run(
                () => v?.ToString() ?? string.Empty,
                string.Empty);
        public static T TryParse<T>(string s)
            => Safe.Run(() => {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFromString(s);
            }, default(T));

        public static object TryParse(string s, Type t)
        //TODO Probably not the best solution, but works for now
            => Safe.Run(() => {
                if (t == typeof(DateTime) || t == typeof(DateTime?)) return DateTime.Parse(s);
                var converter = TypeDescriptor.GetConverter(t);
                if (s.Contains(',') && (t == typeof(double) || t == typeof(double?) || t == typeof(float) || t == typeof(float?))) 
                    s = s.Replace(',', '.');
                return converter.ConvertFromString(null, CultureInfo.InvariantCulture, s);
            }, default);

        public static dynamic Parse(string s, Type t)
            => Safe.Run(() => {
                if (t == typeof(DateTime) || t == typeof(DateTime?)) return DateTime.Parse(s);
                if (t == typeof(bool) || t == typeof(bool?)) return bool.Parse(s);
                if (t == typeof(byte) || t == typeof(byte?)) return byte.Parse(s);
                if (t == typeof(sbyte) || t == typeof(sbyte?)) return sbyte.Parse(s);
                if (t == typeof(short) || t == typeof(short?)) return short.Parse(s);
                if (t == typeof(int) || t == typeof(int?)) return int.Parse(s);
                if (t == typeof(long) || t == typeof(long?)) return long.Parse(s);
                if (t == typeof(decimal) || t == typeof(decimal?)) return decimal.Parse(s);
                if (t == typeof(float) || t == typeof(float?)) return float.Parse(s);
                if (t == typeof(double) || t == typeof(double?)) return double.Parse(s);
                if (t.IsEnum) return Enum.Parse(t, s);
                return TryParse(s, t);
            }, s);
    }
}
